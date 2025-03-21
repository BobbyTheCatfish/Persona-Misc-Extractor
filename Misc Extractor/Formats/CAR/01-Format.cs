using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Reflection.PortableExecutable;
using MiscExtractor;
using MiscExtractor.IO;
using System.Numerics;

namespace Misc_Extractor
{
    public sealed class CarFormat : FileData, ISavable
    {
        public void Save(string path)
        {
            Console.WriteLine(path);
            using (var stream = FileHelper.Create(path))
                Write(new EndianBinaryWriter(stream, Endianness.Big));
        }
        public CarFormat()
        {
        }
        public CarFormat(string path) : this()
        {
            using (var stream = File.OpenRead(path))
                Read(new EndianBinaryReader(stream, Endianness.Big));
        }

        public class Entry()
        {
            public string Title { get; set; }
            public Vector3[] PathNodes { get; set; }
        }

        public Entry[] Entries { get; set; }

        internal override void Read(EndianBinaryReader reader)
        {
            int EntryCount = reader.ReadInt32();
            Entries = new Entry[EntryCount];

            for (int i = 0; i < EntryCount; i++)
            {
                var Title = reader.ReadString(StringBinaryFormat.FixedLength, 32);

                var NodeSize = reader.ReadInt32();
                var NodeCount = reader.ReadInt32();
                var PathNodes = reader.ReadVector3s(NodeCount);
                Entries[i] = new Entry()
                {
                    Title=string.Join("", Title),
                    PathNodes=PathNodes,
                };
                if (PathNodes.Length < 5)
                    reader.ReadVector3s(5 - EntryCount);
                Console.WriteLine(i);
            }
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Entries.Length);
            foreach (var Entry in Entries)
            {
                writer.Write(Entry.Title, StringBinaryFormat.FixedLength, 32);
                writer.Write(64);
                writer.Write(Entry.PathNodes.Length);
                writer.Write(Entry.PathNodes);
                if (Entry.PathNodes.Length < 5)
                {
                    for (int i = 0; i < 5 - Entry.PathNodes.Length; i++)
                        writer.Write(new Vector3(0, 0, 0));
                }
            }
        }
    }
}