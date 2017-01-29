using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveGameFree
{

	/// <summary>
	/// An Interface for Serializers.
	/// You can create your own serializer using this interface.
	/// </summary>
	public interface ISerializer
	{

		/// <summary>
		/// Serialize the specified object to specified file path.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="filePath">File path.</param>
		void Serialize ( object obj, string filePath );

		/// <summary>
		/// Deserialize object from the specified filePath.
		/// </summary>
		/// <param name="filePath">File path.</param>
		T Deserialize<T> ( string filePath );

	}

}