using System;
using System.IO;
using FullSerializer;
using System.Text;
using UnityEngine;

namespace BayatGames.SaveGameFree.Serializers
{

	/// <summary>
	/// Save Game Json Serializer.
	/// </summary>
	public class SaveGameJsonSerializer : ISaveGameSerializer
	{

		/// <summary>
		/// Serialize the specified object to stream with encoding.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="stream">Stream.</param>
		/// <param name="encoding">Encoding.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void Serialize<T> ( T obj, Stream stream, Encoding encoding )
		{
			try
			{
				StreamWriter writer = new StreamWriter ( stream, encoding );
				fsSerializer serializer = new fsSerializer ();
				fsData data = new fsData ();
				serializer.TrySerialize ( obj, out data );
				writer.Write ( fsJsonPrinter.CompressedJson ( data ) );
				writer.Close ();
			}
			catch ( Exception ex )
			{
				Debug.LogException ( ex );
			}
		}

		/// <summary>
		/// Deserialize the specified object from stream using the encoding.
		/// </summary>
		/// <param name="stream">Stream.</param>
		/// <param name="encoding">Encoding.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Deserialize<T> ( Stream stream, Encoding encoding )
		{
			T result = default(T);
			try
			{
				StreamReader reader = new StreamReader ( stream, encoding );
				fsSerializer serializer = new fsSerializer ();
				fsData data = fsJsonParser.Parse ( reader.ReadToEnd () );
				serializer.TryDeserialize ( data, ref result );
				if ( result == null )
				{
					result = default(T);
				}
				reader.Close ();
			}
			catch ( Exception ex )
			{
				Debug.LogException ( ex );
			}
			return result;
		}

	}

}