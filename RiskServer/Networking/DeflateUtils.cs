using System.IO;
using System.IO.Compression;

namespace RiskServer.Networking
{
    /// <summary>
    /// Defalte helper class
    /// </summary>
    public static class DeflateUtils
    {


        public static byte[] ZipStr(string str)
        {
            using (var output = new MemoryStream())
            {
                using (var gzip = new DeflateStream(output, CompressionMode.Compress))
                {
                    using (var writer = new StreamWriter(gzip))
                    {
                        writer.Write(str);
                    }
                }

                return output.ToArray();
            }
        }

        public static string UnZipStr(byte[] input)
        {
            using (var inputStream = new MemoryStream(input))
            {
                using (var gzip = new DeflateStream(inputStream, CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(gzip))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
        public static string UnZipStr(MemoryStream inputStream)
        {
            using (var gzip = new DeflateStream(inputStream, CompressionMode.Decompress))
            {
                using (var reader = new StreamReader(gzip))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
