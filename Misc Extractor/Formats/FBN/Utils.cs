using System.Collections;
using System.Diagnostics;
using MiscExtractor.IO;

namespace MiscExtractor.Formats.FBN
{
    public class Utils
    {
        public class HeaderInfo
        {
            public long start { get; set; }
            public int Version { get; set; }
            public int size { get; set; }
            public int EntryCount { get; set; }
            public uint[] Padding { get; set; } = new uint[3];
        }
        public static void SizeAssert(int Size, long Start, EndianBinaryReader reader)
        {
            Trace.Assert(reader.Position - Size == Start - 4, $"Seek position is {reader.Position} instead of {Start - 4 + Size}");
        }
        public static HeaderInfo GetHeaderInfo(EndianBinaryReader reader)
        {
            HeaderInfo info = new();
            info.start = reader.Position;
            info.Version = reader.ReadInt32();
            info.size = reader.ReadInt32();

            Trace.Assert(reader.ReadInt32() == 16, "Expected ListOffset of 16");
            info.EntryCount = reader.ReadInt32();
            info.Padding = reader.ReadUInt32s(3);
            return info;
        }

        public static List<int> BitFieldRead(EndianBinaryReader reader)
        {
            var v = reader.ReadInt32();
            var bits = new List<int>();
            var binary = Convert.ToString(v, 2).PadLeft(32, '0');
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == '1')
                    bits.Add(i);
            }
            return bits;
        }
        public static int BitFieldWrite(List<int> bitIndexes)
        {
            bool[] bits = [];
            for (int i = 0; i < 32; i++)
            {
                bits[i] = bitIndexes.Contains(i);
            }

            int[] FinalBits = new int[1];
            new BitArray(bits).CopyTo(FinalBits, 0);
            return FinalBits[0];
        }
    }
}
