using System.Diagnostics;
using System.Numerics;
using MiscExtractor.IO;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class WarningObjectData : FileData
    {
        public int Unk1 { get; set; }
        public Vector3 Position { get; set; }
        public Vector4 Rotation { get; set; }
        public short Unk2 { get; set; }
        public short Unk3 { get; set; }
        public short ModelMajorId { get; set; }
        public short ModelMinorId { get; set; }
        public ushort ResourceHandle { get; set; }
        public short Unk4 { get; set; }
        public int Unk5 { get; set; }
        public float ActiveTimer { get; set; }
        public float InactiveTime { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Unk1 = reader.ReadInt32();
            Position = reader.ReadVector3();
            Rotation = reader.ReadVector4();
            Unk2 = reader.ReadInt16();
            Unk3 = reader.ReadInt16();
            ModelMajorId = reader.ReadInt16();
            ModelMinorId = reader.ReadInt16();
            ResourceHandle = reader.ReadUInt16();
            Unk4 = reader.ReadInt16();
            Unk5 = reader.ReadInt32();
            ActiveTimer = reader.ReadSingle();
            InactiveTime = reader.ReadSingle();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Position);
            writer.Write(Rotation);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(ModelMajorId);
            writer.Write(ModelMinorId);
            writer.Write(ResourceHandle);
            writer.Write(Unk4);
            writer.Write(Unk5);
            writer.Write(ActiveTimer);
            writer.Write(InactiveTime);
        }
    }
    public class WarningObject : Block
    {
        public List<WarningObjectData> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            var header = Utils.GetHeaderInfo(reader);
            var EntryCount = header.EntryCount;

            Version = header.Version;

            for (int i = 0; i < EntryCount; i++)
            {
                var Entry = new WarningObjectData();
                Entry.Read(reader);

                Entries.Add(Entry);
            }
            Utils.SizeAssert(header.size, header.start, reader);
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((int)FbnListType.WarningObject);
            writer.Write(Version);
            writer.Write(32 + Entries.Count * 56);
            writer.Write(16);
            writer.Write((int[])[0, 0, 0]);
            foreach (var Entry in Entries)
            {
                Entry.Write(writer);
            }
        }
    }
}
