using System;

namespace MiscExtractor.IO
{
    public static class Utils
    {
        public static float[] ToPercentageBytes(string input) {
            if (input.StartsWith("#"))
            {
                input = input.Substring(1);
            }
            float r = (float)Convert.ToInt32(input.Substring(0, 2), 16) / 255;
            float g = (float)Convert.ToInt32(input.Substring(2, 2), 16) / 255;
            float b = (float)Convert.ToInt32(input.Substring(4, 2), 16) / 255;
            if (input.Length > 6)
            {
                float a = (float)Convert.ToInt32(input.Substring(6, 2), 16) / 255;
                return [r, g, b, a];
            }
            return [r, g, b];
        }
        public static string PercentageToHexString(float[] B)
        {
            int r = (int)(B[0] * 255);
            int g = (int)(B[1] * 255);
            int b = (int)(B[2] * 255);
            if (B.Length > 3)
            {
                int a = (int)(B[3] * 255);
                return $"#{r:X2}{g:X2}{b:X2}{a:X2}";
            }
            return $"#{r:X2}{g:X2}{b:X2}";
        }
        public static byte[] ToBytes(string input)
        {
            if (input.StartsWith("#"))
            {
                input = input.Substring(1);
            }
            byte r = Convert.ToByte(input.Substring(0, 2), 16);
            byte g = Convert.ToByte(input.Substring(2, 2), 16);
            byte b = Convert.ToByte(input.Substring(4, 2), 16);
            if (input.Length > 6)
            {
                byte a = Convert.ToByte(input.Substring(6, 2), 16);
                return [r, g, b, a];
            }
            return [r, g, b];
        }
        public static string ToHexString(byte[] B)
        {
            if (B.Length > 3)
            {
                return $"#{B[0]:X2}{B[1]:X2}{B[2]:X2}{B[3]:X2}";
            }
            return $"#{B[0]:X2}{B[1]:X2}{B[3]:X2}";
        }
    }
    public class Animation
    {
        public int ID { get; set; }
        public int InterpolatedFrames { get; set; }
        public bool Loop { get; set; }
        public float Speed { get; set; }
        public int StartingFrame { get; set; }
        public int EndingFrame { get; set; }
        public int Static1 { get; set; }
        public int Static2 { get; set; }
    }
}
