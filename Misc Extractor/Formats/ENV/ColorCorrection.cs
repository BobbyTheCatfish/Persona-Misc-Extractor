using MiscExtractor.IO;

namespace MiscExtractor.Formats.ENV
{
    public class ColorCorrectionSection : FileData
    {
        public bool DisplayColorGrading { get; set; }
        public float Cyan {  get; set; }
        public float Magenta { get; set; }
        public float Yellow { get; set; }
        public float Dodge { get; set; }
        public float Burn { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            DisplayColorGrading = reader.ReadBoolean();
            Cyan = reader.ReadSingle();
            Magenta = reader.ReadSingle();
            Yellow = reader.ReadSingle();
            Dodge = reader.ReadSingle();
            Burn = reader.ReadSingle();
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(DisplayColorGrading);
            writer.Write(Cyan);
            writer.Write(Magenta);
            writer.Write(Yellow);
            writer.Write(Dodge);
            writer.Write(Burn);
        }
    }
}
