using System.Collections;
using System.Diagnostics;
using System.Numerics;
using MiscExtractor.IO;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class TriggerData : FileData
    {
        public int Unk1 { get; set; }
        public List<int> TypeIdentifierBits { get; set; } = new();
        public Vector3 Center { get; set; }
        public float Unk2 { get; set; }
        public float Unk3 { get; set; }
        public float Unk4 { get; set; }
        public float Scale { get; set; }
        public float Unk5 { get; set; }
        public float RangeRadians { get; set; }
        public Vector3 BottomRight { get; set; }
        public Vector3 TopRight { get; set; }
        public Vector3 BottomLeft { get; set; }
        public Vector3 TopLeft { get; set; }
        public float Unk7 { get; set; }
        public float Unk8 { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Unk1 = reader.ReadInt32();
            TypeIdentifierBits = Utils.BitFieldRead(reader);
            
            Center = reader.ReadVector3();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();
            Scale = reader.ReadSingle();
            Unk5 = reader.ReadSingle();
            RangeRadians = reader.ReadSingle();
            
            BottomRight = reader.ReadVector3();
            TopRight = reader.ReadVector3();
            BottomLeft = reader.ReadVector3();
            TopLeft = reader.ReadVector3();

            Unk7 = reader.ReadSingle();
            Unk8 = reader.ReadSingle();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Unk1);

            writer.Write(Utils.BitFieldWrite(TypeIdentifierBits));

            writer.Write(Center);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(Scale);
            writer.Write(Unk5);
            writer.Write(RangeRadians);
            
            writer.Write(BottomRight);
            writer.Write(TopRight);
            writer.Write(BottomLeft);
            writer.Write(TopLeft);

            writer.Write(Unk7);
            writer.Write(Unk8);
        }
    }
    public class Trigger : BlockWithId
    {

        public List<TriggerData> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            var header = Utils.GetHeaderInfo(reader);
            var EntryCount = header.EntryCount;

            Version = header.Version;

            for (int i = 0; i < EntryCount; i++)
            {
                var Entry = new TriggerData();
                Entry.Read(reader);

                Entries.Add(Entry);
            }
            Utils.SizeAssert(header.size, header.start, reader);
        }
        internal override void Write(FbnListType type, EndianBinaryWriter writer)
        {
            writer.Write((int)type);
            writer.Write(Version);
            writer.Write(32 + Entries.Count * 36);
            writer.Write(16);
            writer.Write((int[])[0, 0, 0]);
            foreach (var Entry in Entries)
            {
                Entry.Write(writer);
            }
        }
    }
}
