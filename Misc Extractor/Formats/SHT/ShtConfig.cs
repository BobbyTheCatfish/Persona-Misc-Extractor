using System.Diagnostics;
using MiscExtractor.IO;

namespace MiscExtractor.Formats.SHT
{
    internal class ShtConfig(): FileData
    {
        public string AWBPath { get; set; }
        public string ACBPath { get; set; }
        public uint Version { get; set; }
        // public uint Reserve {  get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Version = reader.ReadUInt32();
            Trace.Assert(reader.ReadUInt32() == 272, "Expected Section Size of 272");
            Trace.Assert(reader.ReadUInt32() == 0, "Expected Reserve to be 0");
            AWBPath = reader.ReadString(StringBinaryFormat.FixedLength, 128);
            ACBPath = reader.ReadString(StringBinaryFormat.FixedLength, 128);
        }
        internal override void Write(EndianBinaryWriter writer) {
            writer.Write((uint)CommandType.Config);
            writer.Write(Version);
            writer.Write(272); // Section Size (always the same)
            writer.Write(0); // Reserve
            writer.Write(AWBPath, StringBinaryFormat.FixedLength, 128);
            writer.Write(ACBPath, StringBinaryFormat.FixedLength, 128);
        }
    }
}
