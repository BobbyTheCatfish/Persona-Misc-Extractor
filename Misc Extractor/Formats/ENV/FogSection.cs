using MiscExtractor.IO;

namespace MiscExtractor.Formats.ENV
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
            Color = reader.ReadPercentColor();
            EnableFloorFog = reader.ReadBoolean();
            FloorFogStartHeight = reader.ReadSingle();
            FloorFogEndHeight = reader.ReadSingle();
            FloorFogColor = reader.ReadPercentColor();
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Enabled);
            writer.Write(EnableAmbient);
            writer.Write(DisableNonAmbient);
            writer.Write(CameraPlaneSetting);
            writer.Write(StartDistance);
            writer.Write(EndDistance);
            writer.WritePercentColor(Color);
            writer.Write(EnableFloorFog);
            writer.Write(FloorFogStartHeight);
            writer.Write(FloorFogEndHeight);
            writer.WritePercentColor(FloorFogColor);
        }
    }
}
