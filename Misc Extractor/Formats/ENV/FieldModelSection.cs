using System.Numerics;
using MiscExtractor;
using MiscExtractor.IO;

namespace Misc_Extractor.Formats.ENV
{
    public class FieldModelSection : FileData
    {
        public bool UnkBool { get; set; }
        public bool Enabled { get; set; }
        public BSDF FieldShader { get; set; } = new BSDF();
        public Vector3 LightPosition { get; set; }
        public float Field52 { get; set; }
        public float Field56 { get; set; }
        public float Field5A { get; set; }
        public float Field5E { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            UnkBool = reader.ReadBoolean();
            Enabled = reader.ReadBoolean();

            FieldShader.Read(reader);

            Field52 = reader.ReadSingle();
            Field56 = reader.ReadSingle();
            Field5A = reader.ReadSingle();
            Field5E = reader.ReadSingle();

            LightPosition = reader.ReadVector3();

            for (int i = 0; i < 47; i++)
            {
                reader.ReadSingle();
            }
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(UnkBool);
            writer.Write(Enabled);

            FieldShader.Write(writer);

            writer.Write(Field52);
            writer.Write(Field56);
            writer.Write(Field5A);
            writer.Write(Field5E);
            writer.Write(LightPosition);

            for (int i = 0; i < 47; i++)
            {
                writer.Write(0);
            }
        }
    }
}
