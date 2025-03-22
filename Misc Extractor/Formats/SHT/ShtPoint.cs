using MiscExtractor.IO;

namespace MiscExtractor.Formats.SHT
{
    internal class ShtPoint: FileData
    {
        public uint Version { get; set; }
        public uint Reserve1 { get; set; }
        public uint Reserve2 { get; set; }
        public uint Reserve3 { get; set; }
        public uint Reserve4 { get; set; }
        public List<PointData> Data { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            Version = reader.ReadUInt32();
            reader.ReadUInt32();
            Reserve1 = reader.ReadUInt32();

            var EntryCount = reader.ReadUInt32();

            Reserve2 = reader.ReadUInt32();
            Reserve3 = reader.ReadUInt32();
            Reserve4 = reader.ReadUInt32();

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
            writer.Write(Reserve1);
            writer.Write(Data.Count);
            writer.Write(Reserve2);
            writer.Write(Reserve3);
            writer.Write(Reserve4);

            foreach (var Point in Data)
            {
                Point.Write(writer);
            }
        }
    }
}
