using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiscExtractor.IO;

namespace MiscExtractor.Formats.OBL
{
    public class FTDHeader : FileData
    {
        public short Static1 { get; set; }
        public short Static0 { get; set; }
        public int Static32 { get; set; }
        public int Static64 { get; set; }
        public int Field6 { get; set; }
        public int Field7 { get; set; }
        public int Static16 { get; set; }
        public int Field8 { get; set; }
        public int Field9 { get; set; }
        internal int FileSize { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Static1 = reader.ReadInt16();
            Static0 = reader.ReadInt16();
            if (reader.ReadInt32() != 0x46544430) // magic
            {
                throw new Exception("FTD Header Incorrect");
            }
            FileSize = reader.ReadInt32();
            reader.ReadInt32(); // file type
            Static32 = reader.ReadInt32();
            Static64 = reader.ReadInt32();
            Field6 = reader.ReadInt32();
            reader.ReadInt32(); // footer location
            Field7 = reader.ReadInt32();
            Static16 = reader.ReadInt32();
            Field8 = reader.ReadInt32();
            Field9 = reader.ReadInt32();
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Static1);
            writer.Write(Static0);
            writer.Write(0x46544430); // magic
            writer.Write(FileSize);
            writer.Write(0x00000002); // file type
            writer.Write(Static32);
            writer.Write(Static64);
            writer.Write(Field6);
            writer.Write(0); // footer location
            writer.Write(Field7);
            writer.Write(Static16);
            writer.Write(Field8);
            writer.Write(Field9);
        }
    }
}