using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace SaveGameFree.Serializers
{

	/// <summary>
	/// Xml Serialization for Save Game Free.
	/// </summary>
	public class SaveGameFreeXmlSerializer : ISerializer
	{
		
		#region ISerializer implementation

		/// <summary>
		/// Serialize the specified object to specified file path.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="filePath">File path.</param>
		public void Serialize (object obj, string filePath)
		{
			try {
				XmlSerializer serializer = new XmlSerializer (obj.GetType ());
				FileStream file = File.OpenWrite (filePath);
				serializer.Serialize (file, obj);
				file.Close ();
			} catch (Exception ex) {
				Debug.LogException (ex);
			}
		}

		/// <summary>
		/// Deserialize object from the specified filePath.
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Deserialize<T> (string filePath)
		{
			T result = default (T);
			try {
				XmlSerializer serializer = new XmlSerializer (typeof(T));
				FileStream file = File.OpenRead (filePath);
				result = (T)serializer.Deserialize (file);
				file.Close ();
			} catch (Exception ex) {
				Debug.LogException (ex);
			}
			return result;
		}

		#endregion

	}

}