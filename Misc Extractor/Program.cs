using System;
using System.Collections.Generic;
using System.IO;
using Misc_Extractor;
using Newtonsoft.Json;

namespace MiscExtractor
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var DEBUG = false;
            if (DEBUG)
            {
                args = ["C:\\Users\\jaabs\\Downloads\\F000_001_00.HTB.json"];
            }
            var filesettings = StringComparison.InvariantCultureIgnoreCase;
            var supportedFiles = new string[]
            {
                ".ENV",
                ".CAR",
                ".HTB",
                ".JSON",
            };

            if (args.Length == 0)
            {
                var helpLines = new string[] {
                    "Misc Extractor by BobbyTheCatfish",
                    "",
                    "Supported formats:",
                    string.Join("\n", supportedFiles)
                };
                Console.WriteLine(string.Join("\n", helpLines));
                return;
            }

            var path = args[0];

            if (!File.Exists(path))
            {
                Console.WriteLine("Specified file doesn't exist");
                return;
            }

            var extension = Path.GetExtension(path);
            if (!supportedFiles.Contains(extension.ToUpper()))
            {
                Console.WriteLine("Specified file not supported");
                return;
            }

            if (path.EndsWith("json", filesettings))
            {
                var json = File.ReadAllText(path);
                ISavable file;
                var writeSettings = new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error, NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Populate };

                if (path.EndsWith(".env.json", filesettings))
                {
                    file = JsonConvert.DeserializeObject<EnvFormat>(json, writeSettings);
                }
                else if (path.EndsWith(".car.json", filesettings))
                {
                    file = JsonConvert.DeserializeObject<CarFormat>(json, writeSettings);
                }
                else if (path.EndsWith(".htb.json", filesettings))
                {
                    file = JsonConvert.DeserializeObject<HtbFormat>(json, writeSettings);
                }
                else
                {
                    Console.WriteLine("Unrecognized JSON type. Did you alter the extension of this file? Expected name format is 'filename.format.json'");
                    return;
                }
                file.Save(Path.ChangeExtension(path, null));
                return;
            }

            object obj;
            var readSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Populate
            };
            string newExtension;

            if (path.EndsWith("env", filesettings))
            {
                obj = new EnvFormat(path);
                newExtension = "env.json";
            }
            else if (path.EndsWith("car", filesettings))
            {
                obj = new CarFormat(path);
                newExtension = "car.json";
            }
            else if (path.EndsWith("htb", filesettings))
            {
                obj = new HtbFormat(path);
                newExtension = "htb.json";
            }
            else
            {
                Console.WriteLine("Unrecognized file type.");
                return;
            }
            var extracted = JsonConvert.SerializeObject(obj, readSettings);
            File.WriteAllText(Path.ChangeExtension(path, newExtension), extracted);
        }
    }
}