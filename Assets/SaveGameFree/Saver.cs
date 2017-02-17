using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using SaveGameFree.Serializers;

namespace SaveGameFree
{

	/// <summary>
	/// The Available Formats for Storing Data.
	/// </summary>
	public enum Formats
	{
		Binary,
		JSON
	}

	/// <summary>
	/// The Available Paths for Saving and Loading Data.
	/// </summary>
	public enum PathType
	{
		PersistentDataPath,
		DataPath,
		StreamingAssetsPath
	}

	/// <summary>
	/// Main Class API for Saving and Loading Game Data.
	/// </summary>
	public static class Saver
	{

		#region Constants

		/// <summary>
		/// The default json extension.
		/// </summary>
		public const string DefaultJsonExtension = ".json";

		/// <summary>
		/// The default binary extension.
		/// </summary>
		public const string DefaultBinaryExtension = ".bin";

		#endregion

		#region Events

		/// <summary>
		/// Saved callback.
		/// </summary>
		public delegate void SavedCallback ( object obj );

		/// <summary>
		/// Loaded callback.
		/// </summary>
		public delegate void LoadedCallback ( object obj );

		/// <summary>
		/// Occurs when data saved.
		/// </summary>
		public static event SavedCallback OnSaved;

		/// <summary>
		/// Occurs when data loaded.
		/// </summary>
		public static event LoadedCallback OnLoaded;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the current format.
		/// </summary>
		/// <value>The format.</value>
		public static Formats Format { get; set; }

		/// <summary>
		/// Gets or sets the current save path.
		/// </summary>
		/// <value>The save path.</value>
		public static PathType SavePath { get; set; }

		/// <summary>
		/// Gets or sets the current (global) file extension.
		/// </summary>
		/// <value>The file extension.</value>
		public static string FileExtension { get; set; }

		/// <summary>
		/// Gets or sets the current serializer. (Built-in Serializers: BinarySerializer and JsonSerializer)
		/// You can override it by every save function.
		/// </summary>
		/// <value>The serializer.</value>
		public static ISerializer Serializer { get; set; }

		#endregion

		#region Public Variables

		/// <summary>
		/// The application persistent data path, we store it here to make game faster.
		/// Because each time you call Application.persistentDataPath some executions made by unity
		/// that might make the game slower so we store it here.
		/// </summary>
		public static string PersistentDataPath = "";

		/// <summary>
		/// The application data path, we store it here to make game faster.
		/// Because each time you call Application.dataPath some executions made by unity
		/// that might make the game slower so we store it here.
		/// </summary>
		public static string DataPath = "";

		/// <summary>
		/// The application streaming assets path, we store it here to make game faster.
		/// Because each time you call Application.streamingAssetsPath some executions made by unity
		/// that might make the game slower so we store it here.
		/// </summary>
		public static string StreamingAssetsPath = "";

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the save path for the given path type.
		/// </summary>
		/// <returns>The save path.</returns>
		/// <param name="savePath">Save path.</param>
		public static string GetSavePath ( PathType savePath = PathType.PersistentDataPath )
		{
			string path = Application.persistentDataPath;
			if ( savePath == PathType.DataPath )
			{
				path = DataPath;
			}
			else if ( savePath == PathType.PersistentDataPath )
			{
				path = PersistentDataPath;
			}
			else if ( savePath == PathType.StreamingAssetsPath )
			{
				path = StreamingAssetsPath;
			}
			return path;
		}

		/// <summary>
		/// Gets the file path with the given file name and save path type.
		/// </summary>
		/// <returns>The file path.</returns>
		/// <param name="fileName">File name.</param>
		/// <param name="savePath">Save path.</param>
		public static string GetFilePath ( string fileName, PathType savePath )
		{
			return Path.Combine ( GetSavePath ( savePath ), fileName + FileExtension );
		}

		/// <summary>
		/// Gets the file path with the given file name and exetension and save path type.
		/// </summary>
		/// <returns>The file path.</returns>
		/// <param name="fileName">File name.</param>
		/// <param name="extension">Extension. (If you want to override global file extension Saver.FileExtension)</param>
		/// <param name="savePath">Save path.</param>
		public static string GetFilePath ( string fileName, string extension, PathType savePath )
		{
			return Path.Combine ( GetSavePath ( savePath ), fileName + extension );
		}

		/// <summary>
		/// Initializes the Saver with the default configurations.
		/// </summary>
		public static void InitializeDefault ()
		{
			Initialize ( Formats.JSON, ".json", PathType.PersistentDataPath );
		}

		/// <summary>
		/// Initialize the Saver for using.
		/// If you dont call this before using api, this may make some exceptions so
		/// Initialize it before using, See demo folder for usage.
		/// </summary>
		/// <param name="givenFormat">The Format to Store Game Objects by that.</param>
		/// <param name="prefferedExtension">Preffered extension. the global extension, you can override it later.</param>
		/// <param name="savePath">Save path.</param>
		public static void Initialize ( Formats givenFormat, string prefferedExtension, PathType savePath )
		{
			PersistentDataPath = Application.persistentDataPath;
			DataPath = Application.dataPath;
			StreamingAssetsPath = Application.streamingAssetsPath;
			Format = givenFormat;
			switch ( Format )
			{
				case Formats.Binary:
					if ( string.IsNullOrEmpty ( prefferedExtension ) )
					{
						FileExtension = DefaultBinaryExtension;
					}
					Serializer = new BinarySerializer ();
					break;
				default:
				case Formats.JSON:
					if ( string.IsNullOrEmpty ( prefferedExtension ) )
					{
						FileExtension = DefaultBinaryExtension;
					}
					Serializer = new JsonSerializer ();
					break;
			}
			if ( !string.IsNullOrEmpty ( prefferedExtension ) )
			{
				FileExtension = prefferedExtension;
			}
		}

		/// <summary>
		/// Save the specified object with the given file name with the serializer.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="serializer">Serializer.</param>
		public static void Save ( object obj, string fileName, ISerializer serializer )
		{
			string filePath = GetFilePath ( fileName, FileExtension, SavePath );
			serializer.Serialize ( obj, filePath );
			if ( OnSaved != null )
				OnSaved ( obj );
		}

		/// <summary>
		/// Save the specified object with the given file name.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name.</param>
		public static void Save ( object obj, string fileName )
		{
			string filePath = GetFilePath ( fileName, FileExtension, SavePath );
			Serializer.Serialize ( obj, filePath );
			if ( OnSaved != null )
				OnSaved ( obj );
		}

		/// <summary>
		/// Save the specified object with the given file name in the specified save path using the serializer.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name. (myFileName, gameData, data)</param>
		/// <param name="savePath">Save path.</param>
		/// <param name="serializer">Serializer. (Built-in Serializers: BinarySerializer and JsonSerializer)</param>
		public static void Save ( object obj, string fileName, PathType savePath, ISerializer serializer )
		{
			string filePath = GetFilePath ( fileName, FileExtension, savePath );
			serializer.Serialize ( obj, filePath );
			if ( OnSaved != null )
				OnSaved ( obj );
		}

		/// <summary>
		/// Save the specified object with the given file name in the specified save path.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name. (myFileName, gameData, data)</param>
		/// <param name="savePath">Save path.</param>
		public static void Save ( object obj, string fileName, PathType savePath )
		{
			string filePath = GetFilePath ( fileName, FileExtension, savePath );
			Serializer.Serialize ( obj, filePath );
			if ( OnSaved != null )
				OnSaved ( obj );
		}

		/// <summary>
		/// Save the specified object with the given file name and extension in the specified save path using the serializer.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name. (myFileName, gameData, data)</param>
		/// <param name="extension">Extension. (.gd, .bin, .myExtension, .dat, ...)</param>
		/// <param name="savePath">Save path.</param>
		/// <param name="serializer">Serializer.</param>
		public static void Save ( object obj, string fileName, string extension, PathType savePath, ISerializer serializer )
		{
			string filePath = GetFilePath ( fileName, extension, savePath );
			serializer.Serialize ( obj, filePath );
			if ( OnSaved != null )
				OnSaved ( obj );
		}

		/// <summary>
		/// Save the specified object with the given file name and extension in the specified save path.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name. (myFileName, gameData, data)</param>
		/// <param name="extension">Extension. (.gd, .bin, .myExtension, .dat, ...)</param>
		/// <param name="savePath">Save path.</param>
		public static void Save ( object obj, string fileName, string extension, PathType savePath )
		{
			string filePath = GetFilePath ( fileName, extension, savePath );
			Serializer.Serialize ( obj, filePath );
			if ( OnSaved != null )
				OnSaved ( obj );
		}

		/// <summary>
		/// Load the specified file name with the serializer.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="serializer">Serializer.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, ISerializer serializer ) where T : new()
		{
			T obj = new T ();
			string filePath = GetFilePath ( fileName, FileExtension, SavePath );
			if ( !File.Exists ( filePath ) )
			{
				Save ( obj, fileName, FileExtension, SavePath );
				return obj;
			}
			obj = serializer.Deserialize<T> ( filePath );
			if ( OnLoaded != null )
				OnLoaded ( obj );
			return obj;
		}

		/// <summary>
		/// Load the specified file name.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName ) where T : new()
		{
			T obj = new T ();
			Debug.Log ( obj );
			string filePath = GetFilePath ( fileName, FileExtension, SavePath );
			if ( !File.Exists ( filePath ) )
			{
				Save ( obj, fileName, FileExtension, SavePath );
				return obj;
			}
			obj = Serializer.Deserialize<T> ( filePath );
			if ( obj == null )
				obj = new T ();
			if ( OnLoaded != null )
				OnLoaded ( obj );
			return obj;
		}

		/// <summary>
		/// Load the specified file name from the given save path with the serializer.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="savePath">Save path.</param>
		/// <param name="serializer">Serializer.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, PathType savePath, ISerializer serializer ) where T : new()
		{
			T obj = new T ();
			string filePath = GetFilePath ( fileName, FileExtension, savePath );
			if ( !File.Exists ( filePath ) )
			{
				Save ( obj, fileName, FileExtension, savePath, serializer );
				return obj;
			}
			obj = serializer.Deserialize<T> ( filePath );
			if ( obj == null )
				obj = new T ();
			if ( OnLoaded != null )
				OnLoaded ( obj );
			return obj;
		}

		/// <summary>
		/// Load the specified file name from the given save path.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="savePath">Save path.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, PathType savePath ) where T : new()
		{
			T obj = new T ();
			string filePath = GetFilePath ( fileName, FileExtension, savePath );
			if ( !File.Exists ( filePath ) )
			{
				Save ( obj, fileName, FileExtension, savePath );
				return obj;
			}
			obj = Serializer.Deserialize<T> ( filePath );
			if ( obj == null )
				obj = new T ();
			if ( OnLoaded != null )
				OnLoaded ( obj );
			return obj;
		}

		/// <summary>
		/// Load the specified file name and extension from given savePath with the serializer.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="extension">Extension.</param>
		/// <param name="savePath">Save path.</param>
		/// <param name="serializer">Serializer.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, string extension, PathType savePath, ISerializer serializer ) where T : new()
		{
			T obj = new T ();
			string filePath = GetFilePath ( fileName, extension, savePath );
			if ( !File.Exists ( filePath ) )
			{
				Save ( obj, fileName, extension, savePath, serializer );
				return obj;
			}
			obj = serializer.Deserialize<T> ( filePath );
			if ( obj == null )
				obj = new T ();
			if ( OnLoaded != null )
				OnLoaded ( obj );
			return obj;
		}

		/// <summary>
		/// Load the specified file name and extension from given savePath.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="extension">Extension.</param>
		/// <param name="savePath">Save path.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, string extension, PathType savePath ) where T : new()
		{
			T obj = new T ();
			string filePath = GetFilePath ( fileName, extension, savePath );
			if ( !File.Exists ( filePath ) )
			{
				Save ( obj, fileName, extension, savePath );
				return obj;
			}
			obj = Serializer.Deserialize<T> ( filePath );
			if ( obj == null )
				obj = new T ();
			if ( OnLoaded != null )
				OnLoaded ( obj );
			return obj;
		}

		#endregion

	}

}