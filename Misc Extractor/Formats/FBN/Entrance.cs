using System.Diagnostics;
using System.Numerics;
using MiscExtractor.IO;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class EntranceData : FileData
    {
        public int Unk1 { get; set; }
        public int Unk2 { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public short EntranceId { get; set; }
        public short Unk3 { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Unk1 = reader.ReadInt32();
            Unk2 = reader.ReadInt32();
            Position = reader.ReadVector3();
            Rotation = reader.ReadVector3();
            EntranceId = reader.ReadInt16();
            Unk3 = reader.ReadInt16();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Position);
            writer.Write(Rotation);
            writer.Write(EntranceId);
            writer.Write(Unk3);
        }
    }
    public class Entrance : BlockWithId
    {
        public List<EntranceData> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            var header = Utils.GetHeaderInfo(reader);
            var EntryCount = header.EntryCount;

            Version = header.Version;

            for (int i = 0; i < EntryCount; i++)
            {
                var Entry = new EntranceData();
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
            writer.Write(Entries.Count);
            writer.Write((int[])[0, 0, 0]);
            foreach (var Entry in Entries)
            {
                Entry.Write(writer);
            }
        }
    }
}
