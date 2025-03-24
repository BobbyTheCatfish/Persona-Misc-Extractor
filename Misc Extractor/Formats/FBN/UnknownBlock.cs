﻿using System.Text.Json.Serialization;
using MiscExtractor.IO;
using Newtonsoft.Json.Converters;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor.Formats.FBN
{
    public class UnknownBlock : BlockWithId
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public FbnListType Type { get; set; }
        public List<string> RawData {  get; set; }
        public int EntryCount { get; set; }
        public uint[] Padding { get; set; }
        internal override void Read(EndianBinaryReader reader, FbnListType type)
        {
            Type = type;
            var header = Utils.GetHeaderInfo(reader);
            Version = header.Version;
            Padding = header.Padding;
            EntryCount = header.EntryCount;
            var endposition = reader.Position - 32 + header.size;
            while (reader.Position < endposition)
            {

                var entry = reader.ReadInt32s(4);
                RawData.Add(string.Join(" ", entry));
            }
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write((int)Type);
            writer.Write(Version);
            writer.Write(32 + 16 * RawData.Count);
            writer.Write(16);
            writer.Write(EntryCount);
            writer.Write(Padding);
            List<int> nums = new();
            foreach (var entry in RawData)
            {
                foreach (var b in entry.Split(""))
                    nums.Add(Convert.ToInt32(b));
            }
            writer.Write(nums);
            throw new NotImplementedException();
        }
    }
}
