using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace SaveGameFree.Serializers
{

	/// <summary>
	/// Json Serialization for Save Game Free. (Uses Unity JsonUtility)
	/// </summary>
	public class JsonSerializer : ISerializer
	{
		
		#region ISerializer implementation

		/// <summary>
		/// Serialize the specified object to specified file path.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="filePath">File path.</param>
		public void Serialize (object obj, string filePath)
		{
			string data = JsonUtility.ToJson (obj);
			try {
				byte[] bytes = Encoding.UTF8.GetBytes (data);
				data = Convert.ToBase64String (bytes);
				File.WriteAllText (filePath, data);
			} catch (Exception ex) {
				Debug.LogException (ex);
			}
		}

		/// <summary>
		/// Deserialize object from the specified filePath.
		/// </summary>
		/// <param name="filePath">File path.</param>
		public T Deserialize<T> (string filePath)
		{
			T result = default(T);
			try {
				string fileContents = File.ReadAllText (filePath);
				byte[] bytes = Convert.FromBase64String (fileContents);
				fileContents = Encoding.UTF8.GetString (bytes);
				result = JsonUtility.FromJson<T> (fileContents);
			} catch (Exception ex) {
				Debug.LogException (ex);
			}
			return result;
		}

		#endregion

	}

}