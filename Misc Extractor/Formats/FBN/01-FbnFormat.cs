using System.Diagnostics;
using MiscExtractor.Formats.FBN;
using MiscExtractor.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using static MiscExtractor.FbnFormat;

namespace MiscExtractor
{
    public abstract class FileDataWithId : FileData
    {
        internal abstract void Write(FbnListType type, EndianBinaryWriter writer);
    }
    public sealed class FbnFormat : FileData, ISavable
    {
        public class Block() : FileData
        {
            public int Version { get; set; }
            internal override void Read(EndianBinaryReader reader)
            {
                throw new NotImplementedException();
            }
            internal override void Write(EndianBinaryWriter writer)
            {
                throw new NotImplementedException();
            }
        }
        public class BlockWithId() : FileDataWithId
        {
            public int Version { get; set; }
            internal override void Read(EndianBinaryReader reader)
            {
                throw new NotImplementedException();
            }
            internal override void Write(FbnListType type, EndianBinaryWriter writer)
            {
                throw new NotImplementedException();
            }
            internal override void Write(EndianBinaryWriter writer)
            {
                throw new NotImplementedException();
            }
        }
        public void Save(string path)
        {
            using (var stream = FileHelper.Create(path))
                Write(new EndianBinaryWriter(stream, Endianness.Big));
        }
        public FbnFormat()
        {
        }
        public FbnFormat(string path) : this()
        {
            using (var stream = File.OpenRead(path))
                Read(new EndianBinaryReader(stream, Endianness.Big));
        }

        public enum FbnListType
        {
            CrowdSpawn = 2,
            CrowdPath = 7,
            NAVI = 3,
            Entrance = 4,
            MementosEntrance = 13, // 4 Dupe
            MementosEntrance2 = 24, //13 Dupe
            Hit = 5,
            MementosHit = 12, //5 Dupe
            Mask = 6,
            ShadowWander = 8,
            ShadowPatrol = 11,
            Chest = 9,
            Cover = 10,
            NPC = 14,
            StealsObj = 15,
            Steals = 16,
            LightPath = 17,
            SearchObject = 18,
            SearchObjectHit = 19,
            WarningObject = 21, // Lol, lmao
            GrappleObject = 25,
            TriggerSound = 1,
            TriggerVoice = 22,
            TriggerGrapple = 26,
            Header = 1178750512
        }
        public int Version { get; set; }
        public TriggerSound SoundTriggers { get; set; }
        public Entrance Entrances { get; set; }
        public Chest Chests { get; set; }
        public Cover Cover { get; set; }
        public SearchObject SearchObjects { get; set; }
        public PatrolShadow PatrolShadows { get; set; }
        public TriggerSound SearchObjectHits { get; set; }
        public TriggerSound VoiceHits { get; set; }
        public WarningObject WarningObjects { get; set; }
        public NPC NPCs { get; set; }
        public GrappleObject GrappleObjects { get; set; }
        public GrappleTrigger GrappleTriggers { get; set; }
        public Entrance MementosEntrances { get; set; }
        public Entrance MementosEntrances2 { get; set; }
        internal override void Read(EndianBinaryReader reader)
        {
            while (reader.Position < reader.Length)
            {
                int Type = reader.ReadInt32();
                switch ((FbnListType)Type)
                {
                    case FbnListType.Header:
                        var header = new FbnHeader();
                        header.Read(reader);
                        Version = header.Version;
                        break;
                    case FbnListType.TriggerSound:
                        SoundTriggers = new TriggerSound();
                        SoundTriggers.Read(reader);
                        break;
                    case FbnListType.Entrance:
                        Entrances = new Entrance();
                        Entrances.Read(reader);
                        break;
                    case FbnListType.Chest:
                        Chests = new Chest();
                        Chests.Read(reader);
                        break;
                    case FbnListType.Cover:
                        Cover = new Cover();
                        Cover.Read(reader);
                        break;
                    case FbnListType.SearchObject:
                        SearchObjects = new SearchObject();
                        SearchObjects.Read(reader);
                        break;
                    case FbnListType.ShadowPatrol:
                        PatrolShadows = new PatrolShadow();
                        PatrolShadows.Read(reader);
                        break;
                    case FbnListType.SearchObjectHit:
                        SearchObjectHits = new TriggerSound();
                        SearchObjectHits.Read(reader);
                        break;
                    case FbnListType.TriggerVoice:
                        VoiceHits = new TriggerSound();
                        VoiceHits.Read(reader);
                        break;
                    case FbnListType.WarningObject:
                        WarningObjects = new WarningObject();
                        WarningObjects.Read(reader);
                        break;
                    case FbnListType.NPC:
                        NPCs = new NPC();
                        NPCs.Read(reader);
                        break;
                    case FbnListType.GrappleObject:
                        GrappleObjects = new GrappleObject();
                        GrappleObjects.Read(reader);
                        break;
                    case FbnListType.TriggerGrapple:
                        GrappleTriggers = new GrappleTrigger();
                        GrappleTriggers.Read(reader);
                        break;
                    case FbnListType.MementosEntrance:
                        MementosEntrances = new Entrance();
                        MementosEntrances.Read(reader);
                        break;
                    case FbnListType.MementosEntrance2:
                        MementosEntrances2 = new Entrance();
                        MementosEntrances2.Read(reader);
                        break;
                    case FbnListType.CrowdPath:
                        break;
                    case FbnListType.NAVI:
                        break;
                    case FbnListType.CrowdSpawn: break;
                    case FbnListType.Hit: break;
                    case FbnListType.MementosHit: break;
                    case FbnListType.Mask: break;
                    case FbnListType.StealsObj: break;
                    case FbnListType.Steals: break;
                    case FbnListType.LightPath: break;
                    default: throw new Exception("Unknown Instruction");
                }
            }
        }
        internal override void Write(EndianBinaryWriter writer)
        {
            new FbnHeader(Version).Write(writer);
            Chests?.Write(writer);
            WarningObjects?.Write(writer);
            PatrolShadows?.Write(writer);
            MementosEntrances?.Write(FbnListType.MementosEntrance, writer);
            MementosEntrances2?.Write(FbnListType.MementosEntrance2, writer);
        }
    }
}
