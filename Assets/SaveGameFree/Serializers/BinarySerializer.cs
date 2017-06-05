using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveGameFree.Serializers
{

	/// <summary>
	/// Binary Serialization for Save Game Free.
	/// </summary>
	public class BinarySerializer : ISerializer
	{

		#region ISerializer implementation

		/// <summary>
		/// Serialize the specified object to specified file path.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="filePath">File path.</param>
		public void Serialize (object obj, string filePath)
		{
			FileStream file = File.Create (filePath);
			try {
				BinaryFormatter binaryFormatter = new BinaryFormatter ();
				binaryFormatter.Serialize (file, obj);
			} catch (Exception ex) {
				Debug.LogException (ex);
			}
			file.Close ();
		}

		/// <summary>
		/// Deserialize object from the specified filePath.
		/// </summary>
		/// <param name="filePath">File path.</param>
		public T Deserialize<T> (string filePath)
		{
			FileStream file = File.OpenRead (filePath);
			T result = default(T);
			try {
				BinaryFormatter binaryFormatter = new BinaryFormatter ();
				result = (T)binaryFormatter.Deserialize (file);
			} catch (Exception ex) {
				Debug.LogException (ex);
			}
			file.Close ();
			return result;
		}

		#endregion

	}

}