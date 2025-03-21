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
        
        public uint Magic { get; set; }
        public uint GFSVersion { get; set; }
        public uint FileType { get; set; }
        public uint Field1 { get; set; }
        public FieldModelSection FieldModel { get; set; } = new FieldModelSection();
        public CharacterModelSection CharacterModel { get; set; } = new CharacterModelSection();
        public FogSection Fog { get; set; } = new FogSection();
        public LightingSection Lighting { get; set; } = new LightingSection();
        public CameraSection Camera { get; set; } = new CameraSection();
        public FieldShadowSection FieldShadow { get; set; } = new FieldShadowSection();
        public string ShadowColor { get; set; }
        public ColorCorrectionSection ColorCorrection { get; set; } = new ColorCorrectionSection();
        public CharacterOutlineSection CharacterOutline { get; set; } = new CharacterOutlineSection();
        public PhysicsSection Physics { get; set; } = new PhysicsSection();
        public string ClearColor { get; set; }
        public int Field2 { get; set; }
        public int Field3 { get; set; }
        public int Field4 { get; set; }
        public int Field5 { get; set; }
        public int Field6 { get; set; }
        public byte Field7 { get; set; }
        public void Save(string path)
        {
            Console.WriteLine(path);
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
            Magic = reader.ReadUInt32();
            GFSVersion = reader.ReadUInt32();
            FileType = reader.ReadUInt32();
            Field1 = reader.ReadUInt32();

            FieldModel.Read(reader);
            CharacterModel.Read(reader);
            Fog.Read(reader);
            Lighting.Read(reader);
            Camera.Read(reader);
            FieldShadow.Read(reader);
            ShadowColor = reader.ReadPercentColor();
            ColorCorrection.Read(reader);
            CharacterOutline.Read(reader);
            Physics.Read(reader);
            ClearColor = reader.ReadColor();
            Field2 = reader.ReadInt32();
            Field3 = reader.ReadInt32();
            Field4 = reader.ReadInt32();
            Field5 = reader.ReadInt32();
            Field6 = reader.ReadInt32();
            Field7 = reader.ReadByte();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Magic);
            writer.Write( GFSVersion);
            writer.Write( FileType);
            writer.Write( Field1);
            FieldModel?.Write(writer);
            CharacterModel?.Write(writer);
            Fog?.Write(writer);
            Lighting?.Write(writer);
            Camera?.Write(writer);
            FieldShadow?.Write(writer);
            writer.WritePercentColor(ShadowColor);
            ColorCorrection?.Write(writer);
            CharacterOutline?.Write(writer);
            Physics?.Write(writer);
            writer.WriteColor(ClearColor);
            writer.Write(Field2);
            writer.Write(Field3);
            writer.Write(Field4);
            writer.Write(Field5);
            writer.Write(Field6);
            writer.Write(Field7);
        }
    }
}
