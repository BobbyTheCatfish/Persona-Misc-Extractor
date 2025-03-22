using MiscExtractor.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MiscExtractor
{
    public sealed class HtbFormat: FileData, ISavable
    {
        public void Save(string path)
        {
            using (var stream = FileHelper.Create(path))
                Write(new EndianBinaryWriter(stream, Endianness.Big));
        }
        public HtbFormat()
        {
        }
        public HtbFormat(string path) : this()
        {
            using (var stream = File.OpenRead(path))
                Read(new EndianBinaryReader(stream, Endianness.Big));
        }

        public class FBNSettings()
        {
            public int BlockType { get; set; }
            public int Version { get; set; }
            public int ListOffset { get; set; }
        }

        public enum PrompTableEnum
        {
            EXAMINE_fldCheckName0 = 0,
            GO_Blank = 1,
            EXAMINE_fldCheckName1 = 2,
            EXAMINE_fldCheckName2 = 3,
            EXAMINE_fldCheckName3 = 4,
            EXAMINE_fldCheckName4 = 5,
            STEAL_fldCheckName = 6,
            ACTION_fldActionName0 = 7,
            ACTION_fldActionName1 = 8,
            ACTION_fldActionName2 = 9,
            TALK_fldNPCName = 10,
            GO_fldPlaceName = 11,
            SHOP_fldCheckName = 12,
            EXAMINE_fldKFECheckName = 13,
        }
        public class Unused()
        {
            public byte HitIndex { get; set; }
            public byte OverrideType { get; set; }
            public short OverrideNameID { get; set; }
            public int[] Reserve { get; set; }
        }
        public class Flags()
        {
            public string Enabling1 { get; set; }
            public string Enabling2 { get; set; }
            public string Enabling3 { get; set; }
            public string Disabling1 { get; set; }
            public string Disabling2 { get; set; }
            public string Disabling3 { get; set; }
        }
        public class Entry()
        {
            public Flags Flags { get; set; }
            public byte Result { get; set; }
            public byte TriggerType { get; set; }
            public short TextID { get; set; }
            public short ScriptProcedureIndex { get; set; }
            [JsonConverter(typeof(StringEnumConverter))]
            public PrompTableEnum PrompTable { get; set; }
            public Unused Unused { get; set; }
        }

        public FBNSettings FBNHeader { get; set; }
        public Entry[] Entries { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            FBNHeader = new FBNSettings()
            {
                BlockType = reader.ReadInt32(),
                Version = reader.ReadInt32s(2)[0],
                ListOffset = reader.ReadInt32()
            };

            var EntryCount = reader.ReadInt32();
            Entries = new Entry[EntryCount];
            reader.ReadInt32s(3);

            reader.Endianness = Endianness.Little;

            for (int i = 0; i < EntryCount; i++)
            {
                var flags = new Flags()
                {
                    Enabling1 = "0x" + reader.ReadInt32().ToString("X8"),
                    Enabling2 = "0x" + reader.ReadInt32().ToString("X8"),
                    Enabling3 = "0x" + reader.ReadInt32().ToString("X8"),
                    Disabling1 = "0x" + reader.ReadInt32().ToString("X8"),
                    Disabling2 = "0x" + reader.ReadInt32().ToString("X8"),
                    Disabling3 = "0x" + reader.ReadInt32().ToString("X8"),
                };

                Entries[i] = new Entry()
                {
                    Flags = flags,
                    Result = reader.ReadByte(),
                    TriggerType = reader.ReadByte(),
                    TextID = reader.ReadInt16(),
                    ScriptProcedureIndex = reader.ReadInt16(),
                    PrompTable = (PrompTableEnum)reader.ReadInt16(),
                    Unused = new Unused()
                    {
                        HitIndex = reader.ReadByte(),
                        OverrideType = reader.ReadByte(),
                        OverrideNameID = reader.ReadInt16(),
                        Reserve = reader.ReadInt32s(6)
                    }
                };
            }
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(FBNHeader.BlockType);
            writer.Write(FBNHeader.Version);

            var filesize = 32 + 60 * Entries.Length;
            var endpadding = 15 * Entries.Length;
            endpadding = 4 - endpadding % 4;

            // The end padding behavior is kinda weird ngl
            if (endpadding == 4) endpadding = 0;
            else if (endpadding != 2) endpadding += 4;

            filesize += endpadding * 4;

            writer.Write(filesize);
            writer.Write(FBNHeader.ListOffset);
            writer.Write(Entries.Length);
            writer.Write((int[])[0, 0, 0]);
            writer.Endianness = Endianness.Little;
            foreach (var entry in Entries)
            {
                writer.Write(Convert.ToInt32(entry.Flags.Enabling1, 16));
                writer.Write(Convert.ToInt32(entry.Flags.Enabling2, 16));
                writer.Write(Convert.ToInt32(entry.Flags.Enabling3, 16));
                writer.Write(Convert.ToInt32(entry.Flags.Disabling1, 16));
                writer.Write(Convert.ToInt32(entry.Flags.Disabling2, 16));
                writer.Write(Convert.ToInt32(entry.Flags.Disabling3, 16));

                writer.Write(entry.Result);
                writer.Write(entry.TriggerType);
                writer.Write(entry.TextID);
                writer.Write(entry.ScriptProcedureIndex);
                writer.Write((short)entry.PrompTable);
                writer.Write(entry.Unused.HitIndex);
                writer.Write(entry.Unused.OverrideType);
                writer.Write(entry.Unused.OverrideNameID);
                writer.Write(entry.Unused.Reserve);
            }
            for (int i = 0; i < endpadding; i++)
            {
                writer.Write(0);
            }
        }
    }
}
