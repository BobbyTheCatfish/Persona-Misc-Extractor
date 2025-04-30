using MiscExtractor;
using MiscExtractor.Formats.OBL;
using MiscExtractor.IO;
using System.Numerics;

namespace Misc_Extractor
{
        public sealed class OblFormat : FileData, ISavable
    {
        public void Save(string path)
        {
            using (var stream = FileHelper.Create(path))
                Write(new EndianBinaryWriter(stream, Endianness.Big));
        }
        public OblFormat()
        {
        }
        public OblFormat(string path) : this()
        {
            using (var stream = File.OpenRead(path))
                Read(new EndianBinaryReader(stream, Endianness.Big));
        }

        public FTDHeader TableHeader { get; set; } = new();
        public OBLHeader Header { get; set; } = new();
        public List<int> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            TableHeader.Read(reader);
            Header.Read(reader);
            int EntryCount = Header.EntryCount;
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            TableHeader.FileSize = 48 + 32 + Entries.Count * 12;
            TableHeader.Write(writer);

            Header.EntryCount = Entries.Count;
            Header.Write(writer);
        }
    }
}