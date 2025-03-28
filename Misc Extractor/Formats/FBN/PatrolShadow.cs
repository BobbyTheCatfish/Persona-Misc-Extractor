using System.Diagnostics;
using System.Numerics;
using MiscExtractor.IO;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class PathNode(Vector3 pos, uint WaitTime)
    {
        public float X { get; set; } = pos.X;
        public float Y { get; set; } = pos.Y;
        public float Z { get; set; } = pos.Z;
        public uint WaitTime { get; set; } = WaitTime;
    }
    public class PatrolShadowData: FileData
    {
        public int Unk1 { get; set; }
        public float Speed { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int Unk5 { get; set; }
        public short Unk6 { get; set; }
        public PathNode[] PathNodes { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Unk1 = reader.ReadInt32();
            Speed = reader.ReadSingle();
            Unk2 = reader.ReadInt32();
            Unk3 = reader.ReadInt32();
            Unk4 = reader.ReadInt32();
            Unk5 = reader.ReadInt32();

            var NodeCount = reader.ReadInt16();
            PathNodes = new PathNode[NodeCount];

            Unk6 = reader.ReadInt16();

            for (int i = 0; i < NodeCount; i++)
                PathNodes[i] = new PathNode(reader.ReadVector3(), 0);
            
            for (int i = 0; i < NodeCount; i++)
                PathNodes[i].WaitTime = reader.ReadUInt32();
            
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Speed);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(Unk5);
            writer.Write(PathNodes.Length);
            writer.Write(Unk6);

            foreach (var node in PathNodes)
                writer.Write(new Vector3(node.X, node.Y, node.Z));

            foreach (var node in PathNodes)
                writer.Write(node.WaitTime);
        }
    }
    public class PatrolShadow : Block
    {
        public List<PatrolShadowData> Entries { get; set; } = new();
        internal override void Read(EndianBinaryReader reader)
        {
            var header = Utils.GetHeaderInfo(reader);
            var EntryCount = header.EntryCount;

            Version = header.Version;

            for (int i = 0; i < EntryCount; i++)
            {
                var Entry = new PatrolShadowData();
                Entry.Read(reader);

                Entries.Add(Entry);
            }
            Utils.SizeAssert(header.size, header.start, reader);
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((int)FbnListType.ShadowPatrol);
            writer.Write(Version);
            int size = 32;
            foreach (var entry in Entries)
            {
                size += 28 + entry.PathNodes.Length * 16;
            }
            writer.Write(size);
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
