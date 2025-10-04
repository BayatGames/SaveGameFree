using System;
using System.IO;
using FullSerializer;
using System.Text;
using UnityEngine;

namespace Bayat.Unity.SaveGameFree.Serializers
{

	/// <summary>
	/// Save Game Json Serializer.
	/// </summary>
	public class SaveGameJsonSerializer : ISaveGameSerializer
	{

		/// <summary>
		/// Serialize the specified object to stream with encoding.
		/// </summary>
		public void Serialize<T>(T obj, Stream stream, Encoding encoding)
		{
			#if !UNITY_WSA || !UNITY_WINRT
			try
			{
				using (var writer = new StreamWriter(stream, encoding, 1024, leaveOpen: true))
				{
					fsSerializer serializer = new fsSerializer();
					serializer.TrySerialize(obj, out fsData data);
					string compressedJson = fsJsonPrinter.CompressedJson(data);
					writer.Write(compressedJson);
					writer.Flush();
				}
			}
			catch(Exception ex)
			{
				Debug.LogError($"Failed to serialize object of type {typeof(T).Name}: {ex.Message}");
				Debug.LogException(ex);
				throw;
			}
			#else
			using (var writer = new StreamWriter(stream, encoding, 1024, leaveOpen: true))
			{
				writer.Write(JsonUtility.ToJson(obj));
				writer.Flush();
			}
			#endif
		}

		/// <summary>
		/// Deserialize the specified object from stream using the encoding.
		/// </summary>
		/// <param name="stream">Stream.</param>
		/// <param name="encoding">Encoding.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Deserialize<T>(Stream stream, Encoding encoding)
		{
			T result = default;
			#if !UNITY_WSA || !UNITY_WINRT
			try
			{
				using (var reader = new StreamReader(stream, encoding, true, 1024, leaveOpen: true))
				{
					fsSerializer serializer = new fsSerializer();
					fsData data = fsJsonParser.Parse(reader.ReadToEnd());
					serializer.TryDeserialize(data, ref result);
					result ??= default;
				}
			}
			catch(Exception ex)
			{
				Debug.LogError($"Failed to deserialize object of type {typeof(T).Name}: {ex.Message}");
				Debug.LogException(ex);
				throw;
			}
			#else
			using (var reader = new StreamReader(stream, encoding, true, 1024, leaveOpen: true))
			{
				result = JsonUtility.FromJson<T>(reader.ReadToEnd());
			}
			#endif
			return result;
		}

	}

}