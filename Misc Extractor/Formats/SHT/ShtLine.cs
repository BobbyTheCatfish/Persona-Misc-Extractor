using System.Diagnostics;
using MiscExtractor.IO;

namespace MiscExtractor.Formats.SHT
{
    internal class LineData : FileData
    {
        public int Flags { get; set; }
        public float InteriorRadius { get; set; }
        public float ExteriorRadius { get; set; }
        public int CueId { get; set; }
        public int Unk1 { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int Unk5 { get; set; }
        public short Unk6 { get; set; }
        public short Unk7 { get; set; }
        public float Unk8 { get; set; }
        public float Unk9 { get; set; }
        public float Unk10 { get; set; }
        public float Unk11 { get; set; }
        public float Unk12 { get; set; }
        public float Unk13 { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Flags = reader.ReadInt32();
            InteriorRadius = reader.ReadSingle();
            ExteriorRadius = reader.ReadSingle();
            CueId = reader.ReadInt32();
            Unk1 = reader.ReadInt32();
            Unk2 = reader.ReadInt32();
            Unk3 = reader.ReadInt32();
            Unk4 = reader.ReadInt32();
            Unk5 = reader.ReadInt32();
            Unk6 = reader.ReadInt16();
            Unk7 = reader.ReadInt16();
            Unk8 = reader.ReadSingle();
            Unk9 = reader.ReadSingle();
            Unk10 = reader.ReadSingle();
            Unk11 = reader.ReadSingle();
            Unk12 = reader.ReadSingle();
            Unk13 = reader.ReadSingle();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Flags);
            writer.Write(InteriorRadius);
            writer.Write(ExteriorRadius);
            writer.Write(CueId);
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(Unk5);
            writer.Write(Unk6);
            writer.Write(Unk7);
            writer.Write(Unk8);
            writer.Write(Unk9);
            writer.Write(Unk10);
            writer.Write(Unk11);
            writer.Write(Unk12);
            writer.Write(Unk13);
        }
    }
    internal class ShtLine: FileData
    {
        public uint Version { get; set; }
        // public uint Reserve1 { get; set; }
        // public uint Reserve2 { get; set; }
        // public uint Reserve3 { get; set; }
        // public uint Reserve4 { get; set; }
        public List<LineData> Data { get; set; } = new();

        internal override void Read(EndianBinaryReader reader)
        {
            Version = reader.ReadUInt32();
            reader.ReadUInt32();
            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve1 to be 0");
            var entries = reader.ReadUInt32();

            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve2 to be 0");
            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve3 to be 0");
            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve4 to be 0");

            for (int i = 0; i < entries; i++)
            {
                var entry = new LineData();
                entry.Read(reader);
                Data.Add(entry);
            }
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((uint)CommandType.Line);
            writer.Write(Version);
            writer.Write(Data.Count * 64 + 32);
            writer.Write(0);
            writer.Write(Data.Count);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            foreach (var entry in Data)
            {
                entry.Write(writer);
            }
        }
    }
}
