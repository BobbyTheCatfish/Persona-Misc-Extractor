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
    public class FogSection : FileData
    {
        public bool Enabled { get; set; }
        public bool EnableAmbient {  get; set; }
        public bool DisableNonAmbient { get; set; }
        public bool CameraPlaneSetting { get; set; }
        public float StartDistance { get; set; }
        public float EndDistance { get; set; }
        public string Color {  get; set; }
        public bool EnableFloorFog { get; set; }
        public float FloorFogStartHeight { get; set; }
        public float FloorFogEndHeight { get; set; }
        public string FloorFogColor { get; set; }

        internal override void Read(EndianBinaryReader reader)
        {
            Enabled = reader.ReadBoolean();
            EnableAmbient = reader.ReadBoolean();
            DisableNonAmbient = reader.ReadBoolean();
            CameraPlaneSetting = reader.ReadBoolean();
            StartDistance = reader.ReadSingle();
            EndDistance = reader.ReadSingle();
            Color = reader.ReadColor();
            EnableFloorFog = reader.ReadBoolean();
            FloorFogStartHeight = reader.ReadSingle();
            FloorFogEndHeight = reader.ReadSingle();
            FloorFogColor = reader.ReadColor();
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Enabled);
            writer.Write(EnableAmbient);
            writer.Write(DisableNonAmbient);
            writer.Write(CameraPlaneSetting);
            writer.Write(StartDistance);
            writer.Write(EndDistance);
            writer.WriteColor(Color);
            writer.Write(EnableFloorFog);
            writer.Write(FloorFogStartHeight);
            writer.Write(FloorFogEndHeight);
            writer.WriteColor(FloorFogColor);
        }
    }
}
