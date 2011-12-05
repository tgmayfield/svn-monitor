namespace SVNMonitor.Helpers
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class SerializationHelper
    {
        public static object BinaryDeserialize(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream reader = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return formatter.Deserialize(reader);
            }
        }

        public static byte[] BinarySerialize(object graph)
        {
            byte[] buffer;
            lock (graph)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream writer = new MemoryStream())
                {
                    formatter.Serialize(writer, graph);
                    buffer = new byte[writer.Length];
                    writer.Read(buffer, 0, (int) writer.Length);
                }
            }
            return buffer;
        }

        public static void BinarySerialize(object graph, string fileName)
        {
            lock (graph)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream writer = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    formatter.Serialize(writer, graph);
                }
            }
        }

        public static T XmlDeserialize<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xml))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        public static T XmlFileDeserialize<T>(string fileName)
        {
            object obj;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                obj = serializer.Deserialize(reader);
            }
            return (T) obj;
        }

        public static void XmlFileSerialize<T>(T obj, string fileName)
        {
            lock (obj)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                using (XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    serializer.Serialize(writer, obj, namespaces);
                }
            }
        }

        public static string XmlSerialize<T>(T obj)
        {
            lock (obj)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                StringBuilder builder = new StringBuilder();
                using (StringWriter writer = new StringWriter(builder))
                {
                    serializer.Serialize((TextWriter) writer, obj);
                }
                return builder.ToString();
            }
        }
    }
}

