using System.Numerics;
using MiscExtractor;
using MiscExtractor.IO;

namespace Misc_Extractor.Formats.ENV
{
    public class CharacterModelSection : FileData
    {
        public bool UnkBool { get; set; }
        public bool Enabled { get; set; }
        public BSDF CharacterShader { get; set; }
        public Vector3 LightPosition { get; set; }
        public float Field1 { get; set; }
        public float Field2 { get; set; }
        public float Field3 { get; set; }
        public float Field4 { get; set; }
        public float Field5 { get; set; }
        public float NearClip { get; set; }
        public float FarClip { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            UnkBool = reader.ReadBoolean();
            Enabled = reader.ReadBoolean();

            var shader = new BSDF();
            shader.Read(reader);
            CharacterShader = shader;

            Field1 = reader.ReadSingle();
            Field2 = reader.ReadSingle();
            Field3 = reader.ReadSingle();
            Field4 = reader.ReadSingle();

            LightPosition = reader.ReadVector3();
            Field5 = reader.ReadSingle();

            NearClip = reader.ReadSingle();
            FarClip = reader.ReadSingle();
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(UnkBool);
            writer.Write(Enabled);

            CharacterShader.Write(writer);

            writer.Write(Field1);
            writer.Write(Field2);
            writer.Write(Field3);
            writer.Write(Field4);

            writer.Write(LightPosition);

            writer.Write(Field5);

            writer.Write(NearClip);
            writer.Write(FarClip);
        }
    }
}
