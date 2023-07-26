using System;
using System.Data.SqlTypes;
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
        /// Deserialize byte array to object
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T? Deserialize<T>(this byte[]? bytes)
        {
            if (bytes == null)
                return default;
            T @object;
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(bytes))
            {
                stream.Seek(0, 0);
#               pragma warning disable SYSLIB0011  // Legacy type
                try
                {
                    @object = (T)formatter.Deserialize(stream);
                } catch (Exception) { return default; }
#               pragma warning restore SYSLIB0011
            }
            return @object;
        }
    }
}
