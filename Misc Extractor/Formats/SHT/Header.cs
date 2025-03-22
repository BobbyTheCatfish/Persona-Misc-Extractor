using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiscExtractor.IO;

namespace MiscExtractor.Formats.SHT
{
    internal class Header: FileData
    {
        public uint Signiture {  get; set; }
        public uint Version { get; set; }
        public uint Unk1 { get; set; }
        public uint Unk2 { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            Signiture = reader.ReadUInt32();
            Version = reader.ReadUInt32();
            Unk1 = reader.ReadUInt32();
            Unk2 = reader.ReadUInt32();
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Signiture);
            writer.Write(Version);
            writer.Write(Unk1);
            writer.Write(Unk2);
        }
    }
}
