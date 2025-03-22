using MiscExtractor;
using MiscExtractor.IO;

namespace Misc_Extractor.Formats.ENV
{
    public class FieldShadowSection : FileData
    {
        public float FarClip {  get; set; }
        public float Field1 {  get; set; }
        public float AmbientShadowBrightness { get; set; }
        public float Field2 { get; set; }
        public int Field3 { get; set; }
        public float NearClip { get; set; }
        public float Brightness { get; set; }
        public bool Field4 { get; set; }
        public bool Field5 { get; set; }
        public bool Field6 { get; set; }
        public bool Field7 { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            FarClip = reader.ReadSingle();
            Field1 = reader.ReadSingle();
            AmbientShadowBrightness = reader.ReadSingle();
            Field2 = reader.ReadSingle();
            Field3 = reader.ReadInt32();
            NearClip = reader.ReadSingle();
            Brightness = reader.ReadSingle();
            Field4 = reader.ReadBoolean();
            Field5 = reader.ReadBoolean();
            Field6 = reader.ReadBoolean();
            Field7 = reader.ReadBoolean();
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(FarClip);
            writer.Write(Field1);
            writer.Write(AmbientShadowBrightness);
            writer.Write(Field2);
            writer.Write(Field3);
            writer.Write(NearClip);
            writer.Write(Brightness);
            writer.Write(Field4);
            writer.Write(Field5);
            writer.Write(Field6);
            writer.Write(Field7);
        }
    }
}
