using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiscExtractor.IO;

namespace MiscExtractor.Formats.OBL
{
    public class OBLHeader : FileData
    {
        public short Field1 { get; set; }
        public short Reserve2 { get; set; }
        public int Reserve3 { get; set; }
        public int Reserve4 { get; set; }
        public int Reserve5 { get; set; }
        public int Reserve6 { get; set; }
        internal int EntryCount { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            if (reader.ReadInt32() != 0x4F424C30) // magic
            {
                throw new Exception("OBL Header Magic Incorrect");
            }
            Field1 = reader.ReadInt16();
            Reserve2 = reader.ReadInt16();
            Reserve3 = reader.ReadInt32();
            Reserve4 = reader.ReadInt32();
            Reserve5 = reader.ReadInt32();
            reader.ReadInt32(); // section length
            EntryCount = reader.ReadInt32();

            Reserve6 = reader.ReadInt32();
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(0x4F424C30); // magic
            writer.Write(Field1);
            writer.Write(Reserve2);
            writer.Write(Reserve3);
            writer.Write(Reserve4);
            writer.Write(Reserve5);
            writer.Write(12); // section length
            writer.Write(EntryCount);
            writer.Write(Reserve6);
        }
    }
}
