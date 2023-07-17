using System.Text;

namespace NSFW
{
    public static class Data
    {
        public static byte[] Bytes(this string line) =>
            Encoding.Unicode.GetBytes(line);

        public static string String(this byte[] bytes) =>
            Encoding.Unicode.GetString(bytes);
    }
}
