using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiscExtractor;
using MiscExtractor.IO;

namespace Misc_Extractor.Formats.ENV
{
    public class BSDF() : FileData
    {
        public string Ambient {  get; set; }
        public string Diffuse { get; set; }
        public string Specular { get; set; }
        public string Emissive { get; set; }

        internal override void Read(EndianBinaryReader reader)
        {
            Ambient = reader.ReadColor();
            Diffuse = reader.ReadColor();
            Specular = reader.ReadColor();
            Emissive = reader.ReadColor();
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.WriteColor(Ambient);
            writer.WriteColor(Diffuse);
            writer.WriteColor(Specular);
            writer.WriteColor(Emissive);
        }
    }
}
