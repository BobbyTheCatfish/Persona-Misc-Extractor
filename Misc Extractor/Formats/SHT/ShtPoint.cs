using System.Diagnostics;
using MiscExtractor.IO;

namespace MiscExtractor.Formats.SHT
{
    internal class ShtPoint: FileData
    {
        public uint Version { get; set; }
        // public uint Reserve1 { get; set; }
        // public uint Reserve2 { get; set; }
        // public uint Reserve3 { get; set; }
        // public uint Reserve4 { get; set; }
        public List<PointData> Data { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            Version = reader.ReadUInt32();
            reader.ReadUInt32();
            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve1 to be 0");

            var EntryCount = reader.ReadUInt32();

            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve2 to be 0");
            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve3 to be 0");
            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve4 to be 0");

            for (int i = 0; i < EntryCount; i++)
            {
                var Entry = new PointData();
                Entry.Read(reader);
                Data.Add(Entry);
            }
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((uint)CommandType.Point);
            writer.Write(Version);
            writer.Write(Data.Count * 40 + 32 );
            writer.Write(0);
            writer.Write(Data.Count);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);

            foreach (var Point in Data)
            {
                Point.Write(writer);
            }
        }
    }
}
