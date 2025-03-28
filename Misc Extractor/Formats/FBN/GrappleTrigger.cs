using System.Collections;
using System.Diagnostics;
using System.Numerics;
using MiscExtractor.IO;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class GrappleTriggerData : FileData
    {
        public int Unk1 { get; set; }
        public List<int> TypeIdentifierBits { get; set; } = new();
        public ushort ResourceHandle { get; set; }
        public short Unk2 { get; set; }
        public Vector3 Center { get; set; }
        public float Unk3 { get; set; }
        public float Unk4 { get; set; }
        public float Unk5 { get; set; }
        public float Scale { get; set; }
        public float Unk6 { get; set; }
        public float Unk7 { get; set; }
        public Vector3 BottomRight { get; set; }
        public Vector3 TopRight { get; set; }
        public Vector3 BottomLeft { get; set; }
        public Vector3 TopLeft { get; set; }
        // might be range in degrees??? one value is 360 but i haven't researched any other values
        public short RangeDegrees { get; set; }
        public short Unk9 { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Unk1 = reader.ReadInt32();
            TypeIdentifierBits = Utils.BitFieldRead(reader);
            ResourceHandle = reader.ReadUInt16();
            Unk2 = reader.ReadInt16();
            Center = reader.ReadVector3();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();
            Unk5 = reader.ReadSingle();
            Scale = reader.ReadSingle();
            Unk6 = reader.ReadSingle();
            Unk7 = reader.ReadSingle();

            BottomRight = reader.ReadVector3();
            TopRight = reader.ReadVector3();
            BottomLeft = reader.ReadVector3();
            TopLeft = reader.ReadVector3();

            RangeDegrees = reader.ReadInt16();
            Unk9 = reader.ReadInt16();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Unk1);

            writer.Write(Utils.BitFieldWrite(TypeIdentifierBits));

            writer.Write(ResourceHandle);
            writer.Write(Unk2);
            writer.Write(Center);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(Unk5);
            writer.Write(Scale);
            writer.Write(Unk6);
            writer.Write(Unk7);

            writer.Write(BottomRight);
            writer.Write(TopRight);
            writer.Write(BottomLeft);
            writer.Write(TopLeft);

            writer.Write(RangeDegrees);
            writer.Write(Unk9);
        }
    }
    public class GrappleTrigger : Block
    {

        public List<GrappleTriggerData> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            var header = Utils.GetHeaderInfo(reader);
            var EntryCount = header.EntryCount;

            Version = header.Version;

            for (int i = 0; i < EntryCount; i++)
            {
                var Entry = new GrappleTriggerData();
                Entry.Read(reader);

                Entries.Add(Entry);
            }
            Utils.SizeAssert(header.size, header.start, reader);
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((int)FbnListType.TriggerGrapple);
            writer.Write(Version);
            writer.Write(32 + Entries.Count * 100);
            writer.Write(16);
            writer.Write(Entries.Count);
            writer.Write((int[])[0, 0, 0]);
            foreach (var Entry in Entries)
            {
                Entry.Write(writer);
            }
        }
    }
}
