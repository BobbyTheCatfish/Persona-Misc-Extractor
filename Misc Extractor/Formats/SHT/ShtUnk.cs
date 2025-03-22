using System.Diagnostics;
using MiscExtractor.IO;

namespace MiscExtractor.Formats.SHT
{
    internal class UnkData : FileData
    {
        public int CueID { get; set; }
        public int Unk1 { get; set; }
        public float Unk2 { get; set; }
        public float Unk3 { get; set; }
        public float Unk4 { get; set; }
        public float Unk5 { get; set; }
        public int Unk6 { get; set; }
        public int Unk7 { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Unk1 = reader.ReadInt32();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();
            Unk5 = reader.ReadSingle();
            Unk6 = reader.ReadInt32();
            CueID = reader.ReadInt32();
            Unk7 = reader.ReadInt32();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(Unk5);
            writer.Write(Unk6);
            writer.Write(CueID);
            writer.Write(Unk7);
        }
    }
    internal class ShtUnk : FileData
    {
        public uint Version { get; set; }
        // public uint Reserve1 { get; set; }
        // public uint Reserve2 { get; set; }
        // public uint Reserve3 { get; set; }
        // public uint Reserve4 { get; set; }
        public List<UnkData> Data { get; set; } = new();

        internal override void Read(EndianBinaryReader reader)
        {
            Version = reader.ReadUInt32();
            reader.ReadUInt32(); // section size

            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve1 to be 0");

            var EntryCount = reader.ReadUInt32();

            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve2 to be 0");
            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve3 to be 0");
            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve4 to be 0");

            for (int i = 0; i < EntryCount; i++)
            {
                var entry = new UnkData();
                entry.Read(reader);
                Data.Add(entry);
            }
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((uint)CommandType.Unk);
            writer.Write(Version);
            writer.Write(32 * Data.Count + 32);
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
