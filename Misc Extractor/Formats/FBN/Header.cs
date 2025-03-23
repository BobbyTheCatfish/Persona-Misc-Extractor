using System.Diagnostics;
using MiscExtractor.IO;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class FbnHeader : Block
    {
        public FbnHeader(int version = 0) {
            Version = version;
        }
        internal override void Read(EndianBinaryReader reader)
        {
            var start = reader.Position;
            Version = reader.ReadInt32();
            var Size = reader.ReadInt32();
            Trace.Assert(reader.ReadInt32() == 0, "Expected ListOffset of 0");
            Utils.SizeAssert(Size, start, reader);
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((int)FbnListType.Header);
            writer.Write(Version);
            writer.Write(16);
            writer.Write(0);
        }
    }
}
