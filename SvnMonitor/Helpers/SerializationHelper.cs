using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using System.Xml;
using System.Text;

namespace SVNMonitor.Helpers
{
public class SerializationHelper
{
	public SerializationHelper()
	{
	}

	public static object BinaryDeserialize(string fileName)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		using (Stream reader = new FileStream(fileName, FileMode.Open, FileAccess.Read))
		{
			return formatter.Deserialize(reader);
		}
	}

	public static void BinarySerialize(object graph, string fileName)
	{
		Monitor.Enter(object obj = graph);
		try
		{
			BinaryFormatter formatter = new BinaryFormatter();
			using (Stream writer = new FileStream(fileName, FileMode.Create, FileAccess.Write))
			{
				formatter.Serialize(writer, graph);
			}
		}
		finally
		{
			Monitor.Exit(obj);
		}
	}

	public static byte[] BinarySerialize(object graph)
	{
		byte[] buffer;
		Monitor.Enter(object obj = graph);
		try
		{
			BinaryFormatter formatter = new BinaryFormatter();
			using (Stream writer = new MemoryStream())
			{
				formatter.Serialize(writer, graph);
				buffer = new byte[(IntPtr)writer.Length];
				writer.Read(buffer, 0, (int)writer.Length);
			}
		}
		finally
		{
			Monitor.Exit(obj);
		}
		return buffer;
	}

	public static T XmlDeserialize<T>(string xml)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(T));
		using (StringReader reader = new StringReader(xml))
		{
			object obj = serializer.Deserialize(reader);
			return (T)obj;
		}
	}

	public static T XmlFileDeserialize<T>(string fileName)
	{
		object obj = null;
		XmlSerializer serializer = new XmlSerializer(typeof(T));
		using (XmlTextReader reader = new XmlTextReader(fileName))
		{
			return serializer.Deserialize(reader);
		}
		return (T)obj;
	}

	public static void XmlFileSerialize<T>(T obj, string fileName)
	{
		Monitor.Enter(object obj2 = obj);
		try
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
		finally
		{
			Monitor.Exit(obj2);
		}
	}

	public static string XmlSerialize<T>(T obj)
	{
		Monitor.Enter(object obj2 = obj);
		try
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			StringBuilder builder = new StringBuilder();
			using (StringWriter writer = new StringWriter(builder))
			{
				serializer.Serialize(writer, obj);
			}
			string xml = builder.ToString();
			return xml;
		}
		finally
		{
			Monitor.Exit(obj2);
		}
	}
}
}