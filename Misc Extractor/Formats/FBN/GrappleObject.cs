using System.Diagnostics;
using System.Numerics;
using MiscExtractor.IO;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class GrappleObjectData : FileData
    {
        public int Unk1 { get; set; }
        public bool[] JumpAfterGrapple { get; set; }
        public short WireSequenceID { get; set; }
        public Vector3 Position { get; set; }
        public Vector4 Rotation { get; set; }
        public short ModelMajorId { get; set; }
        public short ModelMinorId { get; set; }
        public ushort ResourceHandle { get; set; }
        public ushort Unk2 { get; set; }
        public ushort JumpLength { get; set; }
        public ushort JumpHeight { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Unk1 = reader.ReadInt32();
            JumpAfterGrapple = reader.ReadBooleans(2);
            WireSequenceID = reader.ReadInt16();
            Position = reader.ReadVector3();
            Rotation = reader.ReadVector4();
            ModelMajorId = reader.ReadInt16();
            ModelMinorId = reader.ReadInt16();
            ResourceHandle = reader.ReadUInt16();
            Unk2 = reader.ReadUInt16();
            JumpLength = reader.ReadUInt16();
            JumpHeight = reader.ReadUInt16();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Unk1);
            Trace.Assert(JumpAfterGrapple.Length == 2, "Expected array of 2 for JumpAfterGrapple");
            writer.Write(JumpAfterGrapple);
            writer.Write(WireSequenceID);
            writer.Write(Position);
            writer.Write(Rotation);
            writer.Write(ModelMajorId);
            writer.Write(ModelMinorId);
            writer.Write(ResourceHandle);
            writer.Write(Unk2);
            writer.Write(JumpLength);
            writer.Write(JumpHeight);
        }
    }
    public class GrappleObject : Block
    {
        public List<GrappleObjectData> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            var header = Utils.GetHeaderInfo(reader);
            var EntryCount = header.EntryCount;

            Version = header.Version;

            for (int i = 0; i < EntryCount; i++)
            {
                var Entry = new GrappleObjectData();
                Entry.Read(reader);

                Entries.Add(Entry);
            }
            Utils.SizeAssert(header.size, header.start, reader);
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((int)FbnListType.GrappleObject);
            writer.Write(Version);
            writer.Write(32 + Entries.Count * 48);
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
