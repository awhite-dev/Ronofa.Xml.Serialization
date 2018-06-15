using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Ronofa.Tools
{
    /// <summary>
    /// This class handles all serialize and deserialize actions against objects and files.
    /// </summary>
    public static class GenericSerializer
    {
        #region Enumeration
        public enum FormatterType { Binary, Xml }
        #endregion

        #region Serialize Methods
        /// <summary>
        /// This method serializes the specified <paramref name="obj"/> into a string.
        /// </summary>
        /// <typeparam name="T">The generic type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="formatter">The method of serialization to use.</param>
        /// <returns></returns>
        public static string Serialize<T>(T obj, FormatterType formatter)
        {
            return Serialize<T>(obj, formatter, null, null);
        }

        /// <summary>
        /// This method serializes the specified <paramref name="obj"/> into a string.
        /// </summary>
        /// <typeparam name="T">The generic type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="formatter">The method of serialization to use.</param>
        /// <param name="namespaces"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj, FormatterType formatter, XmlSerializerNamespaces namespaces, XmlWriterSettings settings)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(string.Format("Unable to serialize '{0}'.", obj));
            }
            string xml = string.Empty;
            switch (formatter)
            {
                case FormatterType.Binary:
                    {
                        xml = Formatters.GenericBinaryFormatter.Serialize<T>(obj);
                        break;
                    }
                case FormatterType.Xml:
                    {
                        xml = Formatters.GenericXmlFormatter.Serialize<T>(obj, namespaces, settings);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException(string.Format("Invalid Formatter option."));
                    }
            }
            return xml;
        }

        /// <summary>
        /// This method serializes the specified <paramref name="obj"/> into a file.
        /// </summary>
        /// <typeparam name="T">The generic type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="formatter">The method of serialization to use.</param>
        /// <param name="filePath"></param>
        public static void Serialize<T>(T obj, FormatterType formatter, string filePath)
        {
            Serialize<T>(obj, formatter, filePath, null, null);
        }

        /// <summary>
        /// This method serializes the specified <paramref name="obj"/> into a file.
        /// </summary>
        /// <typeparam name="T">The generic type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="formatter">The method of serialization to use.</param>
        /// <param name="filePath"></param>
        /// <param name="namespaces"></param>
        /// <param name="encoding"></param>
        public static void Serialize<T>(T obj, FormatterType formatter, string filePath, XmlSerializerNamespaces namespaces, Encoding encoding)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(string.Format("Unable to serialize '{0}'.", obj));
            }
            switch (formatter)
            {
                case FormatterType.Binary:
                    {
                        break;
                    }
                case FormatterType.Xml:
                    {
                        Formatters.GenericXmlFormatter.Serialize<T>(obj, filePath, namespaces, encoding);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion

        #region Deserialize Methods
        /// <summary>
        /// This method deserializes the specified <paramref name="xml"/> string.
        /// </summary>
        /// <typeparam name="T">The generic type of the object.</typeparam>
        /// <param name="xml">The xml to deserialize.</param>
        /// <param name="formatter">The method of deserialization to use.</param>
        /// <returns>A new instance of type <typeparamref name="T"/>.</returns>
        public static T Deserialize<T>(string xml, FormatterType formatter)
        {
            return Deserialize<T>(xml, formatter, null);
        }

        /// <summary>
        /// This method deserializes the specified <paramref name="xml"/> string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="formatter"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml, FormatterType formatter, XmlReaderSettings settings)
        {
           T result;
            switch (formatter)
            {
                case FormatterType.Binary:
                    {
                        result = default(T);
                        break;
                    }
                case FormatterType.Xml:
                    {
                        result = Formatters.GenericXmlFormatter.Deserialize<T>(xml, settings);
                        break;
                    }
                default:
                    {
                        result = default(T);
                        break;
                    }
            }
            return result;
        }

        /// <summary>
        /// This method deserializes a file at the specified <paramref name="filePath"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static T DeserializeFile<T>(string filePath, FormatterType formatter)
        {
            return DeserializeFile<T>(filePath, formatter, null);
        }

        /// <summary>
        /// This method deserializes a file at the specified <paramref name="filePath"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="formatter"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T DeserializeFile<T>(string filePath, FormatterType formatter, Encoding encoding)
        {
            T result;
            switch (formatter)
            {
                case FormatterType.Binary:
                    {
                        result = default(T);
                        break;
                    }
                case FormatterType.Xml:
                    {
                        return Formatters.GenericXmlFormatter.Deserialize<T>(filePath, encoding);
                    }
                default:
                    {
                        result = default(T);
                        break;
                    }
            }
            return result;
        }
        #endregion
    }
}
