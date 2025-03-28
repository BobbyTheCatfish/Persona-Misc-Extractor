using System.Diagnostics;
using System.Numerics;
using MiscExtractor.IO;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class SearchObjectData : FileData
    {
        public int Unk1 { get; set; }
        public short Unk2 { get; set; }
        public short Unk3 { get; set; }
        public Vector3 Position { get; set; }
        public Vector4 Rotation { get; set; }
        public short ModelMajorId { get; set; }
        public short ModelMinorId { get; set; }
        public ushort ResourceHandle { get; set; }
        public short Unk4 { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Unk1 = reader.ReadInt32();
            Unk2 = reader.ReadInt16();
            Unk3 = reader.ReadInt16();
            Position = reader.ReadVector3();
            Rotation = reader.ReadVector4();
            ModelMajorId = reader.ReadInt16();
            ModelMinorId = reader.ReadInt16();
            ResourceHandle = reader.ReadUInt16();
            Unk4 = reader.ReadInt16();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Position);
            writer.Write(Rotation);
            writer.Write(ModelMajorId);
            writer.Write(ModelMinorId);
            writer.Write(ResourceHandle);
            writer.Write(Unk4);
        }
    }
    public class SearchObject : Block
    {
        public List<SearchObjectData> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            var header = Utils.GetHeaderInfo(reader);
            var EntryCount = header.EntryCount;

            Version = header.Version;

            for (int i = 0; i < EntryCount; i++)
            {
                var Entry = new SearchObjectData();
                Entry.Read(reader);

                Entries.Add(Entry);
            }
            Utils.SizeAssert(header.size, header.start, reader);
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((int)FbnListType.SearchObject);
            writer.Write(Version);
            writer.Write(32 + Entries.Count * 44);
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
