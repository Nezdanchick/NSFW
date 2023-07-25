using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NSFW
{
    public static class Data
    {
        /// <summary>
        /// Serialize object to byte array
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static byte[] Serialize(this object target)
        {
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
#               pragma warning disable SYSLIB0011  // Legacy type
                formatter.Serialize(stream, target);
#               pragma warning restore SYSLIB0011
                bytes = stream.ToArray();
            }
            return bytes;
        }
        /// <summary>
        /// Get string from byte array
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this byte[] bytes)
        {
            T @object;
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(bytes))
            {
                stream.Seek(0, 0);
#               pragma warning disable SYSLIB0011  // Legacy type
                @object = (T)formatter.Deserialize(stream);
#               pragma warning restore SYSLIB0011
            }
            return @object;
        }
    }
}
