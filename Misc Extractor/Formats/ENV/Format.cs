using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Misc_Extractor.Formats.ENV;
using MiscExtractor.IO;

namespace MiscExtractor {
    public sealed class EnvFormat : FileData, ISavable
    {
        
        public int Magic { get; set; }
        public int GFSVersion { get; set; }
        public int FileType { get; set; }
        public int Field0C { get; set; }
        public FieldModelSection FieldModelSection { get; set; } = new FieldModelSection();
        public CharacterModelSection CharacterModelSection { get; set; } = new CharacterModelSection();
        public FogSection FogSection { get; set; } = new FogSection();
        public LightingSection LightingSection { get; set; } = new LightingSection();
        public void Save(string path)
        {
            using (var stream = FileHelper.Create(path))
                Write(new EndianBinaryWriter(stream, Endianness.Big));
        }
        public EnvFormat()
        {
        }
        public EnvFormat(string path): this()
        {
            using (var stream = File.OpenRead(path))
                Read(new EndianBinaryReader(stream, Endianness.Big));
        }
        internal override void Read(EndianBinaryReader reader)
        {
            Magic = reader.ReadInt32();
            GFSVersion = reader.ReadInt32();
            FileType = reader.ReadInt32();
            Field0C = reader.ReadInt32();

            FieldModelSection.Read(reader);
            CharacterModelSection.Read(reader);
            FogSection.Read(reader);
            LightingSection.Read(reader);

        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Magic);
            writer.Write( GFSVersion);
            writer.Write( FileType);
            writer.Write( Field0C);
            FieldModelSection?.Write(writer);
            CharacterModelSection?.Write(writer);
            FogSection?.Write(writer);
        }
    }
}
