using System.Collections;
using System.Diagnostics;
using System.Numerics;
using MiscExtractor.IO;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class CoverData : FileData
    {
        public Dictionary<string, bool> Flags { get; set; } = new();
        private Dictionary<int, string> FlagMap { get; set; } = new()
        {
            {24, "LStickAllowed"},
            {25, "InvisibleFromOtherSpots"},
            {26, "VisiblePartyMembers"},
            {28, "EnablePoint2"},
            {29, "EnablePoint1"},
        };
        public int Unk1 { get; set; }
        public Vector3[] JokerPositions { get; set; }
        public Vector3[] JokerRotations { get; set; }
        public Vector3[] PartyRotations { get; set; }
        public Vector3[] IconPositions { get; set; }
        public Vector3[] CameraRotations { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Unk4 { get; set; }
        public short CameraHeightMode { get; set; }
        public short ConnectedPointID { get; set; }
        public short Unk5 { get; set; }
        public short Unk6 { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            var value = Convert.ToString(reader.ReadInt32(), 2).PadLeft(32);

            for (int i = 0; i < value.Length; i++)
            {
                FlagMap.TryGetValue(i, out string name);
                name ??= $"Flag{i}";
                Flags.Add(name, value[i] == '1');
            }
            Unk1 = reader.ReadInt32();

            JokerPositions = reader.ReadVector3s(2);
            PartyRotations = reader.ReadVector3s(2);
            IconPositions = reader.ReadVector3s(2);
            CameraRotations = reader.ReadVector3s(2);

            Unk2 = reader.ReadInt32();
            Unk3 = reader.ReadInt32();
            Unk4 = reader.ReadInt32();

            CameraHeightMode = reader.ReadInt16();
            ConnectedPointID = reader.ReadInt16();
            Unk5 = reader.ReadInt16();
            Unk6 = reader.ReadInt16();

            JokerRotations = reader.ReadVector3s(2);
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            bool[] bits = [];
            for (int i = 0; i < 32; i++)
            {
                var (key, flag) = Flags.ElementAt(i);
                int realKey;
                if (key.StartsWith("Flag"))
                    realKey = Convert.ToInt32(key[4]);
                else
                    realKey = FlagMap.FirstOrDefault(x => x.Value == key).Key;
                if (flag == true)
                    bits[realKey] = true;
                else
                    bits[realKey] = false;
            }

            int[] FinalBits = new int[1];
            new BitArray(bits).CopyTo(FinalBits, 0);
            writer.Write(FinalBits[0]);

            writer.Write(Unk1);

            writer.Write(JokerPositions);
            writer.Write(PartyRotations);
            writer.Write(IconPositions);
            writer.Write(CameraRotations);

            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(CameraHeightMode);
            writer.Write(ConnectedPointID);
            writer.Write(Unk5);
            writer.Write(Unk6);

            writer.Write(JokerRotations);
        }
    }
    public class Cover : Block
    {
        public List<CoverData> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            var header = Utils.GetHeaderInfo(reader);
            var EntryCount = header.EntryCount;

            Version = header.Version;

            for (int i = 0; i < EntryCount; i++)
            {
                var Entry = new CoverData();
                Entry.Read(reader);

                Entries.Add(Entry);
            }
            Utils.SizeAssert(header.size, header.start, reader);
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((int)FbnListType.Cover);
            writer.Write(Version);
            writer.Write(32 + Entries.Count * 56);
            writer.Write(16);
            writer.Write((int[])[0, 0, 0]);
            foreach (var Entry in Entries)
            {
                Entry.Write(writer);
            }
        }
    }
}
