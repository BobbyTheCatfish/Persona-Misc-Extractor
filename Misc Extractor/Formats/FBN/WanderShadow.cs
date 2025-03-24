using System.Diagnostics;
using System.Numerics;
using MiscExtractor.IO;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class WanderShadowData : FileData
    {
        public int Unk1 { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public float WanderRadius { get; set; }
        public uint Unk4;
        internal override void Read(EndianBinaryReader reader)
        {
            Unk1 = reader.ReadInt32();
            Unk2 = reader.ReadInt32();
            Unk3 = reader.ReadInt32();
            Position = reader.ReadVector3();
            Rotation = reader.ReadVector3();
            WanderRadius = reader.ReadSingle();
            Unk4 = reader.ReadUInt32();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Position);
            writer.Write(Rotation);
            writer.Write(WanderRadius);
            writer.Write(Unk4);
        }
    }
    public class WanderShadow : Block
    {
        public List<WanderShadowData> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            var header = Utils.GetHeaderInfo(reader);
            var EntryCount = header.EntryCount;

            Version = header.Version;

            for (int i = 0; i < EntryCount; i++)
            {
                var Entry = new WanderShadowData();
                Entry.Read(reader);

                Entries.Add(Entry);
            }
            Utils.SizeAssert(header.size, header.start, reader);
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((int)FbnListType.ShadowPatrol);
            writer.Write(Version);
            writer.Write(32 + Entries.Count * 52);
            writer.Write(16);
            writer.Write((int[])[0, 0, 0]);
            foreach (var Entry in Entries)
            {
                Entry.Write(writer);
            }
        }
    }
}
