using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using MiscExtractor;
using MiscExtractor.IO;

namespace Misc_Extractor.Formats.ENV
{
    public class FieldModelSection : FileData
    {
        public bool UnkBool { get; set; }
        public bool Enabled { get; set; }
        public BSDF CharacterShader { get; set; } = new BSDF();
        public Vector3 LightPosition { get; set; }
        public float Field52 { get; set; }
        public float Field56 { get; set; }
        public float Field5A { get; set; }
        public float Field5E { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            UnkBool = reader.ReadBoolean();
            Enabled = reader.ReadBoolean();

            CharacterShader.Read(reader);

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

            CharacterShader.Write(writer);

            writer.Write(LightPosition);
            writer.Write(Field52);
            writer.Write(Field56);
            writer.Write(Field5A);
            writer.Write(Field5E);

            for (int i = 0; i < 47; i++)
            {
                writer.Write(0);
            }
        }
    }
}
