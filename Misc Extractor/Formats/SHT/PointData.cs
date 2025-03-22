using System.Numerics;
using MiscExtractor.IO;

namespace MiscExtractor.Formats.SHT
{
    internal class PointData : FileData
    {
        public int Flags {  get; set; }
        public int Type { get; set; }
        public Vector3 Center { get; set; }
        public float InteriorRadius { get; set; }
        public float ExteriorRadius { get; set; }
        public int SoundCueID { get; set; }
        public int Field1 { get; set; }
        public int Field2 { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Flags = reader.ReadInt32();
            Type = reader.ReadInt32();
            Center = reader.ReadVector3();
            InteriorRadius = reader.ReadSingle();
            ExteriorRadius = reader.ReadSingle();
            SoundCueID = reader.ReadInt32();
            Field1 = reader.ReadInt32();
            Field2 = reader.ReadInt32();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Flags);
            writer.Write(Type);
            writer.Write(Center);
            writer.Write(InteriorRadius);
            writer.Write(ExteriorRadius);
            writer.Write(SoundCueID);
            writer.Write(Field1);
            writer.Write(Field2);
        }
    }
}
