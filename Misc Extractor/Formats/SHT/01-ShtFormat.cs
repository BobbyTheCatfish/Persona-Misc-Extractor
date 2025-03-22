using System.Diagnostics;
using MiscExtractor.Formats.SHT;
using MiscExtractor.IO;

namespace MiscExtractor
{
    public enum CommandType
    {
        Config = 1,
        Point = 2,
        Line = 3,
        Unk = 4
    }
    internal class ShtFormat: FileData, ISavable
    {
        public void Save(string path)
        {
            Console.WriteLine(path);
            using (var stream = FileHelper.Create(path))
                Write(new EndianBinaryWriter(stream, Endianness.Big));
        }
        public ShtFormat()
        {
        }
        public ShtFormat(string path) : this()
        {
            using (var stream = File.OpenRead(path))
                Read(new EndianBinaryReader(stream, Endianness.Big));
        }

        public Header Header { get; set; } = new Header();
        public ShtConfig Config { get; set; }
        public ShtLine Lines { get; set; }
        public ShtPoint Points { get; set; }
        public ShtUnk Unk { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Header.Read(reader);
            while (reader.Position < reader.BaseStream.Length)
            {
                var Type = reader.ReadUInt32();
                switch (Type)
                {
                    case 1:
                        Config = new ShtConfig();
                        Config.Read(reader);
                        break;
                    case 2: 
                        Points = new ShtPoint();
                        Points.Read(reader);
                        break;
                    case 3:
                        Lines = new ShtLine();
                        Lines.Read(reader);
                        break;
                    case 4:
                        Unk = new ShtUnk();
                        Unk.Read(reader);
                        break;
                    default: throw new Exception("Unrecognized Command");
                }
            }
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            Trace.Assert(Header != null, "Header not found");
            Header.Write(writer);

            Trace.Assert(!(Config == null && Points == null && Lines == null && Unk == null), "Expected some Points, Lines, or Unk.");

            Config?.Write(writer);
            Points?.Write(writer);
            Lines?.Write(writer);
            Unk?.Write(writer);
        }
    }
}
