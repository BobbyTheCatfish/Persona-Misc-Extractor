using MiscExtractor.IO;

namespace MiscExtractor.Formats.SHT
{
    internal class ShtConfig(): FileData
    {
        public string AWBPath { get; set; }
        public string ACBPath { get; set; }
        public uint Version { get; set; }
        public uint Reserve {  get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Version = reader.ReadUInt32();
            reader.ReadUInt32();
            Reserve = reader.ReadUInt32();
            AWBPath = reader.ReadString(StringBinaryFormat.FixedLength, 128);
            ACBPath = reader.ReadString(StringBinaryFormat.FixedLength, 128);
        }
        internal override void Write(EndianBinaryWriter writer) {
            writer.Write((uint)CommandType.Config);
            writer.Write(Version);
            writer.Write(272);
            writer.Write(Reserve);
            writer.Write(AWBPath, StringBinaryFormat.FixedLength, 128);
            writer.Write(ACBPath, StringBinaryFormat.FixedLength, 128);
        }
    }
}
