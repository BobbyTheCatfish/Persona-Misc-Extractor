using MiscExtractor.IO;

namespace MiscExtractor
{
    public abstract class FileData
    {
        internal abstract void Read(EndianBinaryReader reader);

        internal abstract void Write(EndianBinaryWriter writer);
    }
}