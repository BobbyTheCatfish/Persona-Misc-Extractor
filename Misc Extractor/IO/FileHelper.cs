﻿using System.IO;

namespace MiscExtractor.IO
{
    public static class FileHelper
    {
        public static FileStream Create( string path )
        {
            var directory = Path.GetDirectoryName( path );
            if ( !string.IsNullOrWhiteSpace(directory) )
                Directory.CreateDirectory( directory );

            return File.Create( path );
        }
    }
}
