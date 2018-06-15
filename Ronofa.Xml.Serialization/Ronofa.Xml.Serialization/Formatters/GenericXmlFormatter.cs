using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Ronofa.Tools.Formatters
{
    /// <summary>
    /// This class handles the serialization and deserialization using the System.Xml.Serialization namespace.
    /// </summary>
    internal static class GenericXmlFormatter
    {
        /// <summary>
        /// This method serializes the specified <paramref name="obj"/> into a string.
        /// </summary>
        /// <typeparam name="T">The generic type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="settings">The XmlWriterSettings instance.</param>
        /// <param name="namespaces">The XmlSerializerNamespaces instance.</param>
        /// <returns>An XML string.</returns>
        public static string Serialize<T>(T obj, XmlSerializerNamespaces namespaces, XmlWriterSettings settings)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {                    
                    serializer.Serialize(xmlWriter, obj, namespaces);
                }
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// This method serializes the specified <paramref name="obj"/> into a file.
        /// </summary>
        /// <typeparam name="T">The generic type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="filePath">The full file path.</param>
        /// <param name="encoding">The Encoding to use.</param>
        /// <param name="namespaces">The XmlSerializerNamespaces instance.</param>
        public static void Serialize<T>(T obj, string filePath, XmlSerializerNamespaces namespaces, Encoding encoding)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                using (var writer = new StreamWriter(stream, encoding)) 
                {                    
                    serializer.Serialize(stream, obj, namespaces);
                }
            }
        }

        /// <summary>
        /// This method deserializes the specified <paramref name="xml"/> string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml, XmlReaderSettings settings)
        {
            T result;
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(xml))
            {
                using (var xmlReader = XmlReader.Create(xml, settings))
                {
                    result = (T)serializer.Deserialize(reader);
                }
            }
            return result;
        }

        /// <summary>
        /// This method deserializes a file at the specified <paramref name="filePath"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string filePath, Encoding encoding)
        {
            T result;
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(filePath))
            {
                using (var xmlReader = XmlReader.Create(reader))
                {
                    result = (T)serializer.Deserialize(xmlReader, encoding.EncodingName);
                }
            }
            return result;
        }
    }
}
