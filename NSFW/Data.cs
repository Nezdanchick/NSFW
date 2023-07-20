using System.Text;

namespace NSFW
{
    public static class Data
    {
        /// <summary>
        /// Get byte array from string
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static byte[] Bytes(this string line) =>
            Encoding.Unicode.GetBytes(line);
        /// <summary>
        /// Get string from byte array
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string String(this byte[] bytes) =>
            Encoding.Unicode.GetString(bytes);
    }
}
