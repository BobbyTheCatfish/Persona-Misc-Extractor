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
    public class Bloom()
    {
        public float Amount { get; set; }
        public float Detail { get; set; }
        public float WhiteLevel { get; set; }
        public float DarkLevel { get; set; }
    }

    public class Glare()
    {
        public float Sensitivity { get; set; }
        public float Length { get; set; }
        public float ChromaticAberration { get; set; }
        public float Direction { get; set; }
        public int GlareMode { get; set; }
    }

    public class LightingSection : FileData
    {
        public bool EnableHDRGraphicalOutput { get; set; }
        public bool EnableToneMapBloom { get; set; }
        public bool EnableStarFilterGlare { get; set; }
        public bool Field1 { get; set; }
        public bool Field2 { get; set; }
        public bool Field3 { get; set; }
        public bool Field4 { get; set; }
        public int Field5 { get; set; }
        public Bloom Bloom { get; set; }
        public Glare Glare { get; set; }
        public float SceneWhiteLevels { get; set; }
        public float SceneDarkLevels { get; set; }
        public float Field6 { get; set; }
        public float Field7 { get; set; }
        public float Field8 { get; set; }
        public int Field9 { get; set; }
        public float Field10 { get; set; }
        public float Field11 { get; set; }
        public int Field12 { get; set; }
        public int Field13 { get; set; }
        public float Field14 { get; set; }
        public float Field15 { get; set; }
        public string ColorBoost {  get; set; }
        public float Field16 { get; set; }
        public float Field17 { get; set; }
        public float Field18 { get; set; }
        public float Field19 { get; set; }
        public float Field20 { get; set; }
        public float Field21 { get; set; }
        public float Field22 { get; set; }


        internal override void Read(EndianBinaryReader reader)
        {
            EnableHDRGraphicalOutput = reader.ReadBoolean();
            EnableToneMapBloom = reader.ReadBoolean();
            EnableStarFilterGlare = reader.ReadBoolean();
            Field1 = reader.ReadBoolean();
            Field2 = reader.ReadBoolean();
            Field3 = reader.ReadBoolean();
            Field4 = reader.ReadBoolean();
            Field5 = reader.ReadInt32();

            Bloom = new Bloom()
            {
                Amount = reader.ReadSingle(),
                Detail = reader.ReadSingle(),
                WhiteLevel = reader.ReadSingle(),
                DarkLevel = reader.ReadSingle(),
            };

            Glare = new Glare()
            {
                Sensitivity = reader.ReadSingle()
            };

            SceneWhiteLevels = reader.ReadSingle();
            SceneDarkLevels = reader.ReadSingle();

            Field6 = reader.ReadSingle();
            Field7 = reader.ReadSingle();
            Field8 = reader.ReadSingle();
            
            Field9 = reader.ReadInt32();
            
            Field10 = reader.ReadSingle();
            Field11 = reader.ReadSingle();

            Field12 = reader.ReadInt32();
            Field13 = reader.ReadInt32();

            Field14 = reader.ReadSingle();
            Field15 = reader.ReadSingle();

            ColorBoost = reader.ReadColor(false);
            
            Field16 = reader.ReadSingle();
            Field17 = reader.ReadSingle();
            Field18 = reader.ReadSingle();
            Field19 = reader.ReadSingle();
            Field20 = reader.ReadSingle();
            Field21 = reader.ReadSingle();
            Field22 = reader.ReadSingle();

            Glare.Length = reader.ReadSingle();
            Glare.ChromaticAberration = reader.ReadSingle();
            Glare.Direction = reader.ReadSingle();
            Glare.GlareMode = reader.ReadInt32();
        }

        internal override void Write(EndianBinaryWriter writer)
        {

            writer.Write(EnableHDRGraphicalOutput);
            writer.Write(EnableToneMapBloom);
            writer.Write(EnableStarFilterGlare);
            writer.Write(Field1);
            writer.Write(Field2);
            writer.Write(Field3);
            writer.Write(Field4);
            writer.Write(Field5);

            writer.Write(Bloom.Amount);
            writer.Write(Bloom.Detail);
            writer.Write(Bloom.WhiteLevel);
            writer.Write(Bloom.DarkLevel);

            writer.Write(Glare.Sensitivity);

            writer.Write(SceneWhiteLevels);
            writer.Write(SceneDarkLevels);
            writer.Write(Field6);
            writer.Write(Field7);
            writer.Write(Field8);
            writer.Write(Field9);
            writer.Write(Field10);
            writer.Write(Field11);
            writer.Write(Field12);
            writer.Write(Field13);
            writer.Write(Field14);
            writer.Write(Field15);

            writer.WriteColor(ColorBoost);


            writer.Write(Field16);
            writer.Write(Field17);
            writer.Write(Field18);
            writer.Write(Field19);
            writer.Write(Field20);
            writer.Write(Field21);
            writer.Write(Field22);

            writer.Write(Glare.Length);
            writer.Write(Glare.ChromaticAberration);
            writer.Write(Glare.Direction);
            writer.Write(Glare.GlareMode);
        }
    }
}
