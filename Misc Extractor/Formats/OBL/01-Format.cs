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
        public class Entry : FileData
        {
            public short Static512 { get; set; }
            public ushort ResourceHandler { get; set; }
            public short Field3 { get; set; }
            public short CollectedBitFlag { get; set; }
            public short Field5 { get; set; }
            public short Field6 { get; set; }
            internal override void Read(EndianBinaryReader reader)
            {
                ResourceHandler = reader.ReadUInt16();
                Static512 = reader.ReadInt16();
                Field3 = reader.ReadInt16();
                CollectedBitFlag = reader.ReadInt16();
                Field5 = reader.ReadInt16();
                Field6 = reader.ReadInt16();
            }
            internal override void Write(EndianBinaryWriter writer)
            {
                writer.Write(ResourceHandler);
                writer.Write(Static512);
                writer.Write(Field3);
                writer.Write(CollectedBitFlag);
                writer.Write(Field5);
                writer.Write(Field6);
            }
        }
        public FTDHeader TableHeader { get; set; } = new();
        public OBLHeader Header { get; set; } = new();
        public List<Entry> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            TableHeader.Read(reader);
            Header.Read(reader);
            int EntryCount = Header.EntryCount;

            for (int i = 0; i < EntryCount; i++)
            {
                Entry entry = new ();
                entry.Read(reader);
                Entries.Add(entry);
            }
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            TableHeader.FileSize = 48 + 32 + Entries.Count * 12;
            TableHeader.Write(writer);

            Header.EntryCount = Entries.Count;
            Header.Write(writer);

            foreach (Entry entry in Entries)
            {
                entry.Write(writer);
            }
        }
    }
}