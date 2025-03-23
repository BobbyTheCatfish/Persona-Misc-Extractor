using MiscExtractor.IO;

namespace MiscExtractor.Formats.ENV
{
    public class BSDF() : FileData
    {
        public string Ambient {  get; set; }
        public string Diffuse { get; set; }
        public string Specular { get; set; }
        public string Emissive { get; set; }

        internal override void Read(EndianBinaryReader reader)
        {
            Ambient = reader.ReadPercentColor();
            Diffuse = reader.ReadPercentColor();
            Specular = reader.ReadPercentColor();
            Emissive = reader.ReadPercentColor();
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.WritePercentColor(Ambient);
            writer.WritePercentColor(Diffuse);
            writer.WritePercentColor(Specular);
            writer.WritePercentColor(Emissive);
        }
    }
}
