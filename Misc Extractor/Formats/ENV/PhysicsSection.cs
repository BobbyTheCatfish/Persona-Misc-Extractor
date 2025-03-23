using System.Numerics;
using MiscExtractor.IO;

namespace MiscExtractor.Formats.ENV
{
    public class Wind()
    {
        public bool Enabled { get; set; }
        public Vector3 DirectionRange { get; set; }
        public float Strength { get; set; }
        public float StrengthModifier { get; set; }
        public float CycleTime { get; set; }
        public float CycleDelay { get; set; }
    }
    public class PhysicsSection : FileData
    {
        public bool Enabled { get; set; }
        public float Gravity { get; set; }
        public Wind Wind { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Enabled = reader.ReadBoolean();
            Gravity = reader.ReadSingle();
            Wind = new Wind()
            {
                Enabled = reader.ReadBoolean(),
                DirectionRange = reader.ReadVector3(),
                Strength = reader.ReadSingle(),
                StrengthModifier = reader.ReadSingle(),
                CycleTime = reader.ReadSingle(),
                CycleDelay = reader.ReadSingle(),
            };
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Enabled);
            writer.Write(Gravity);
            writer.Write(Wind.Enabled);
            writer.Write(Wind.DirectionRange);
            writer.Write(Wind.Strength);
            writer.Write(Wind.StrengthModifier);
            writer.Write(Wind.CycleTime);
            writer.Write(Wind.CycleDelay);
        }
    }
}
