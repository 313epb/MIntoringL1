using System.IO;
using System.Runtime.Serialization;

namespace Task.TestHelpers
{
	public class DataContractSerializerTester<T> : SerializationTester<T, XmlObjectSerializer>
	{
		public DataContractSerializerTester(
			XmlObjectSerializer serializer, bool showResult = false) : base(serializer, showResult)
		{
		}

		internal override T Deserialization(MemoryStream stream)
		{
			return (T) Serializer.ReadObject(stream);
		}

		internal override void Serialization(T data, MemoryStream stream)
		{
			Serializer.WriteObject(stream, data);
		}
	}
}