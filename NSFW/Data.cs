using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NSFW
{
    /// <summary>
    /// Extension class for data serialization and deserialization
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// Serialize object to byte array
        /// </summary>
        /// <param name="target">Object for serialization</param>
        /// <returns>Serialized byte array</returns>
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
        /// <param name="bytes">Array for deserialization</param>
        /// <returns>Deserialized object</returns>
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
                @object = (T)formatter.Deserialize(stream);
#               pragma warning restore SYSLIB0011
            }
            return @object;
        }
    }
}
