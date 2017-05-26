using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

using SaveGameFree.Serializers;

namespace SaveGameFree
{

	/// <summary>
	/// The Available Formats for Storing Data.
	/// </summary>
	public enum FormatType
	{

		/// <summary>
		/// The Binary.
		/// Using System.Runtime.Serialization.Formatters.Binary.BinaryFormatter.
		/// </summary>
		Binary,

		/// <summary>
		/// The JSON.
		/// </summary>
		JSON,

		/// <summary>
		/// The XML.
		/// Using System.Xml.Serialization.XmlSerializer
		/// </summary>
		XML
	}

	/// <summary>
	/// The Available Paths for Saving and Loading Data.
	/// </summary>
	public enum PathType
	{

		/// <summary>
		/// The persistent data path.
		/// Application.persistentDataPath
		/// </summary>
		PersistentDataPath,

		/// <summary>
		/// The data path.
		/// Application.dataPath
		/// </summary>
		DataPath,

		/// <summary>
		/// The streaming assets path.
		/// Application.streamingAssetsPath
		/// </summary>
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
		public const string DEFAULT_JSON_EXTENSION = ".json";

		/// <summary>
		/// The default binary extension.
		/// </summary>
		public const string DEFAULT_BINARY_EXTENSION = ".bin";

		/// <summary>
		/// The default xml extension.
		/// </summary>
		public const string DEFAULT_XML_EXTENSION = ".xml";

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
		/// The saved handler.
		/// </summary>
		public static SavedCallback SavedHandler;

		/// <summary>
		/// The loaded handler.
		/// </summary>
		public static LoadedCallback LoadedHandler;

		/// <summary>
		/// Occurs when data saved.
		/// </summary>
		public static event SavedCallback OnSaved { 
			add {
				if ( SavedHandler == null || SavedHandler.GetInvocationList ().Contains ( value ) )
				{
					SavedHandler += value;
				}
			}
			remove {
				SavedHandler -= value;
			}
		}

		/// <summary>
		/// Occurs when data loaded.
		/// </summary>
		public static event LoadedCallback OnLoaded { 
			add {
				if ( LoadedHandler == null || LoadedHandler.GetInvocationList ().Contains ( value ) )
				{
					LoadedHandler += value;
				}
			}
			remove {
				LoadedHandler -= value;
			}
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// The initialized.
		/// </summary>
		private static bool m_Initialized = false;

		/// <summary>
		/// Gets a value indicating whether this <see cref="SaveGameFree.Saver"/> is initialized.
		/// </summary>
		/// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
		public static bool Initialized {
			get {
				return m_Initialized;
			}
		}

		/// <summary>
		/// Gets or sets the current format.
		/// </summary>
		/// <value>The format.</value>
		public static FormatType Format { get; set; }

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
			return GetFilePath ( fileName, savePath, FileExtension );
		}

		/// <summary>
		/// Gets the file path with the given file name and exetension and save path type.
		/// </summary>
		/// <returns>The file path.</returns>
		/// <param name="fileName">File name.</param>
		/// <param name="savePath">Save path.</param>
		/// <param name="extension">Extension. (If you want to override global file extension Saver.FileExtension)</param>
		public static string GetFilePath ( string fileName, PathType savePath, string extension )
		{
			return Path.GetFullPath ( Path.Combine ( GetSavePath ( savePath ), fileName + extension ) );
		}

		/// <summary>
		/// Initializes the Saver with the default configurations.
		/// </summary>
		[System.Obsolete ( "SaveGameFree InitializeDefault depreacated and will be removed in the next versions. use Initialize instead.", true )]
		public static void InitializeDefault ()
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize Save Game Free using Default configuration.
		/// </summary>
		public static void Initialize ()
		{
			Initialize ( FormatType.JSON, null, PathType.PersistentDataPath, null );
		}

		/// <summary>
		/// Initialize the specified format.
		/// </summary>
		/// <param name="format">Format.</param>
		public static void Initialize ( FormatType format )
		{
			Initialize ( format, null, PathType.PersistentDataPath, null );
		}

		/// <summary>
		/// Initialize the specified serializer.
		/// </summary>
		/// <param name="serializer">Serializer.</param>
		public static void Initialize ( ISerializer serializer )
		{
			Initialize ( FormatType.JSON, serializer, PathType.PersistentDataPath, null );
		}

		/// <summary>
		/// Initialize the specified savePath.
		/// </summary>
		/// <param name="savePath">Save path.</param>
		public static void Initialize ( PathType savePath )
		{
			Initialize ( FormatType.JSON, null, savePath, null );
		}

		/// <summary>
		/// Initialize the specified extension.
		/// </summary>
		/// <param name="extension">Extension.</param>
		public static void Initialize ( string extension )
		{
			Initialize ( FormatType.JSON, null, PathType.PersistentDataPath, extension );
		}

		/// <summary>
		/// Initialize the specified format and serializer.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="serializer">Serializer.</param>
		public static void Initialize ( FormatType format, ISerializer serializer )
		{
			Initialize ( format, serializer, PathType.PersistentDataPath, null );
		}

		/// <summary>
		/// Initialize the specified format and savePath.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="savePath">Save path.</param>
		public static void Initialize ( FormatType format, PathType savePath )
		{
			Initialize ( format, null, savePath, null );
		}

		/// <summary>
		/// Initialize the specified format and extension.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="extension">Extension.</param>
		public static void Initialize ( FormatType format, string extension )
		{
			Initialize ( format, null, PathType.PersistentDataPath, extension );
		}

		/// <summary>
		/// Initialize the specified serializer and savePath.
		/// </summary>
		/// <param name="serializer">Serializer.</param>
		/// <param name="savePath">Save path.</param>
		public static void Initialize ( ISerializer serializer, PathType savePath )
		{
			Initialize ( FormatType.JSON, serializer, savePath, null );
		}

		/// <summary>
		/// Initialize the specified serializer and extension.
		/// </summary>
		/// <param name="serializer">Serializer.</param>
		/// <param name="extension">Extension.</param>
		public static void Initialize ( ISerializer serializer, string extension )
		{
			Initialize ( FormatType.JSON, serializer, PathType.PersistentDataPath, extension );
		}

		/// <summary>
		/// Initialize the specified savePath and extension.
		/// </summary>
		/// <param name="savePath">Save path.</param>
		/// <param name="extension">Extension.</param>
		public static void Initialize ( PathType savePath, string extension )
		{
			Initialize ( FormatType.JSON, null, savePath, extension );
		}

		/// <summary>
		/// Initialize the specified format, serializer and savePath.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="serializer">Serializer.</param>
		/// <param name="savePath">Save path.</param>
		public static void Initialize ( FormatType format, ISerializer serializer, PathType savePath )
		{
			Initialize ( format, serializer, savePath, null );
		}

		/// <summary>
		/// Initialize the specified format, serializer and extension.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="serializer">Serializer.</param>
		/// <param name="extension">Extension.</param>
		public static void Initialize ( FormatType format, ISerializer serializer, string extension )
		{
			Initialize ( format, serializer, PathType.PersistentDataPath, extension );
		}

		/// <summary>
		/// Initialize the specified format, savePath and extension.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="savePath">Save path.</param>
		/// <param name="extension">Extension.</param>
		public static void Initialize ( FormatType format, PathType savePath, string extension )
		{
			Initialize ( format, null, savePath, extension );
		}

		/// <summary>
		/// Initialize the specified format, serializer, savePath and extension.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="serializer">Serializer.</param>
		/// <param name="savePath">Save path.</param>
		/// <param name="extension">Extension.</param>
		public static void Initialize ( FormatType format, ISerializer serializer, PathType savePath, string extension )
		{
			if ( m_Initialized )
			{
				return;
			}
			m_Initialized = true;
			PersistentDataPath = Application.persistentDataPath;
			DataPath = Application.dataPath;
			StreamingAssetsPath = Application.streamingAssetsPath;
			Format = format;
			SavePath = savePath;
			switch ( format )
			{
				default:
				case FormatType.JSON:
					FileExtension = DEFAULT_JSON_EXTENSION;
					Serializer = new JsonSerializer ();
					break;
				case FormatType.Binary:
					FileExtension = DEFAULT_BINARY_EXTENSION;
					Serializer = new BinarySerializer ();
					break;
				case FormatType.XML:
					FileExtension = DEFAULT_XML_EXTENSION;
					Serializer = new XmlSerializer ();
					break;
			}
			if ( serializer != null )
			{
				Serializer = serializer;
			}
			if ( !string.IsNullOrEmpty ( extension ) )
			{
				FileExtension = extension;
			}
		}

		/// <summary>
		/// Save the specified object with the given file name.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name.</param>
		public static void Save ( object obj, string fileName )
		{
			Save ( obj, fileName, Serializer, SavePath );
		}

		/// <summary>
		/// Save the specified object with the given file name with the serializer.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="serializer">Serializer.</param>
		public static void Save ( object obj, string fileName, ISerializer serializer )
		{
			Save ( obj, fileName, Serializer, SavePath );
		}

		/// <summary>
		/// Save the specified object with the given file name in the specified save path.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name. (myFileName, gameData, data)</param>
		/// <param name="savePath">Save path.</param>
		public static void Save ( object obj, string fileName, PathType savePath )
		{
			Save ( obj, fileName, Serializer, savePath );
		}

		/// <summary>
		/// Save the specified object with the given file name in the specified save path using the serializer.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name. (myFileName, gameData, data)</param>
		/// <param name="serializer">Serializer. (Built-in Serializers: BinarySerializer and JsonSerializer)</param>
		/// <param name="savePath">Save path.</param>
		public static void Save ( object obj, string fileName, ISerializer serializer, PathType savePath )
		{
			Save ( obj, fileName, serializer, savePath, FileExtension );
		}

		/// <summary>
		/// Save the specified object with the given file name and extension in the specified save path.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name. (myFileName, gameData, data)</param>
		/// <param name="savePath">Save path.</param>
		/// <param name="extension">Extension. (.gd, .bin, .myExtension, .dat, ...)</param>
		public static void Save ( object obj, string fileName, PathType savePath, string extension )
		{
			Save ( obj, fileName, Serializer, savePath, extension );
		}

		/// <summary>
		/// Save the specified object with the given file name and extension in the specified save path.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name. (myFileName, gameData, data)</param>
		/// <param name="serializer">Serializer (BinarySerializer or JSONSerializer).</param>
		/// <param name="extension">Extension. (.gd, .bin, .myExtension, .dat, ...)</param>
		public static void Save ( object obj, string fileName, ISerializer serializer, string extension )
		{
			Save ( obj, fileName, serializer, SavePath, extension );
		}

		/// <summary>
		/// Save the specified object with the given file name and extension in the specified save path using the serializer.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="fileName">File name. (myFileName, gameData, data)</param>
		/// <param name="serializer">Serializer.</param>
		/// <param name="savePath">Save path.</param>
		/// <param name="extension">Extension. (.gd, .bin, .myExtension, .dat, ...)</param>
		public static void Save ( object obj, string fileName, ISerializer serializer, PathType savePath, string extension )
		{
			string filePath = GetFilePath ( fileName, savePath, extension );
			serializer.Serialize ( obj, filePath );
			if ( SavedHandler != null )
				SavedHandler ( obj );
		}

		/// <summary>
		/// Load the specified file name.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName ) where T : new()
		{
			return Load<T> ( fileName, Serializer, SavePath, FileExtension );
		}

		/// <summary>
		/// Load the specified file name.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="serializer">Serializer.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, ISerializer serializer ) where T : new()
		{
			return Load<T> ( fileName, serializer, SavePath, FileExtension );
		}

		/// <summary>
		/// Load the specified file name.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="savePath">Save path.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, PathType savePath ) where T : new()
		{
			return Load<T> ( fileName, Serializer, savePath, FileExtension );
		}

		/// <summary>
		/// Load the specified file name.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="extension">Extension.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, string extension ) where T : new()
		{
			return Load<T> ( fileName, Serializer, SavePath, extension );
		}

		/// <summary>
		/// Load the specified file name and extension from given savePath.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="serializer">Serializer.</param>
		/// <param name="extension">Extension.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, ISerializer serializer, string extension ) where T : new()
		{
			return Load<T> ( fileName, serializer, SavePath, extension );
		}

		/// <summary>
		/// Load the specified file name and extension from given savePath.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="savePath">Save path.</param>
		/// <param name="extension">Extension.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, PathType savePath, string extension ) where T : new()
		{
			return Load<T> ( fileName, Serializer, savePath, extension );
		}

		/// <summary>
		/// Load the specified file name and extension from given savePath.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="serializer">Serializer.</param>
		/// <param name="savePath">Save path.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, ISerializer serializer, PathType savePath ) where T : new()
		{
			return Load<T> ( fileName, serializer, savePath, FileExtension );
		}

		/// <summary>
		/// Load the specified file name and extension from given savePath with the serializer.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="serializer">Serializer.</param>
		/// <param name="savePath">Save path.</param>
		/// <param name="extension">Extension.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Load<T> ( string fileName, ISerializer serializer, PathType savePath, string extension ) where T : new()
		{
			T obj = new T ();
			string filePath = GetFilePath ( fileName, savePath, extension );
			if ( !File.Exists ( filePath ) )
			{
				Save ( obj, fileName, serializer, savePath, extension );
				return obj;
			}
			obj = serializer.Deserialize<T> ( filePath );
			if ( obj == null )
				obj = new T ();
			if ( LoadedHandler != null )
				LoadedHandler ( obj );
			return obj;
		}

		#endregion

	}

}