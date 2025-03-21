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
    public class CharacterOutlineSection : FileData
    {
        public string LightMap {  get; set; }
        public bool Enabled { get; set; }
        public float Opacity { get; set; }
        public float Width { get; set; }
        public float Brightness { get; set; }
        public float Field1 { get; set; }
        public float Field2 { get; set; }
        public float ReflectionHeight { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            LightMap = reader.ReadPercentColor();
            Enabled = reader.ReadBoolean();
            Opacity = reader.ReadSingle();
            Width = reader.ReadSingle();
            Brightness = reader.ReadSingle();
            Field1 = reader.ReadSingle();
            Field2 = reader.ReadSingle();
            ReflectionHeight = reader.ReadSingle();
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.WritePercentColor(LightMap);
            writer.Write(Enabled);
            writer.Write(Opacity);
            writer.Write(Width);
            writer.Write(Brightness);
            writer.Write(Field1);
            writer.Write(Field2);
            writer.Write(ReflectionHeight);
        }
    }
}
