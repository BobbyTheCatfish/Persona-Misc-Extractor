using System.Diagnostics;
using System.Numerics;
using MiscExtractor.IO;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class NPCData : FileData
    {
        public int Unk1 { get; set; }
        public float Unk2 { get; set; }
        public Vector3 Rotation { get; set; }
        public float Unk3 { get; set; }
        public float Unk4 { get; set; }
        public short FNT_ID { get; set; }
        public short Unk5 { get; set; }
        public int Unk6 { get; set; }
        public int Unk7 { get; set; }
        public int Unk8 { get; set; }
        public short Unk9 { get; set; }
        public Vector3[] PathNodes { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Unk1 = reader.ReadInt32();
            Unk2 = reader.ReadSingle();
            Rotation = reader.ReadVector3();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();
            FNT_ID = reader.ReadInt16();
            Unk5 = reader.ReadInt16();
            Unk6 = reader.ReadInt32();
            Unk7 = reader.ReadInt32();
            Unk8 = reader.ReadInt32();
            var Nodes = reader.ReadInt16();
            Unk9 = reader.ReadInt16();

            PathNodes = reader.ReadVector3s(Nodes);
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Rotation);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(FNT_ID);
            writer.Write(Unk5);
            writer.Write(Unk6);
            writer.Write(Unk7);
            writer.Write(Unk8);
            writer.Write(PathNodes.Length);
            writer.Write(Unk9);
            writer.Write(PathNodes);
        }
    }
    public class NPC : Block
    {
        public List<NPCData> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            var header = Utils.GetHeaderInfo(reader);
            var EntryCount = header.EntryCount;

            Version = header.Version;

            for (int i = 0; i < EntryCount; i++)
            {
                var Entry = new NPCData();
                Entry.Read(reader);

                Entries.Add(Entry);
            }
            Utils.SizeAssert(header.size, header.start, reader);
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            
            writer.Write((int)FbnListType.NPC);
            writer.Write(Version);
            int size = 32;
            foreach (var entry in Entries)
            {
                size += 48 + entry.PathNodes.Length * 12;
            }
            writer.Write(size);
            writer.Write(16);
            writer.Write(Entries.Count);
            writer.Write((int[])[0, 0, 0]);
            foreach (var Entry in Entries)
            {
                Entry.Write(writer);
            }
        }
    }
}
