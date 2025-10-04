using Bayat.Unity.SaveGameFree.Encoders;
using Bayat.Unity.SaveGameFree.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Bayat.Unity.SaveGameFree
{
    /// <summary>
    /// Save Game.
    /// Use these APIs to Save & Load game data.
    /// If you are looking for Web saving and loading use SaveGameWeb.
    /// </summary>
    public static class SaveGame
    {
        /// <summary>
        /// Save handler.
        /// </summary>
        public delegate void SaveHandler(object obj, string identifier, bool encode,
            string password, ISaveGameSerializer serializer, ISaveGameEncoder encoder,
            Encoding encoding, SaveGamePath path);

        /// <summary>
        /// Load handler.
        /// </summary>
        public delegate void LoadHandler(object loadedObj, string identifier, bool encode,
            string password, ISaveGameSerializer serializer, ISaveGameEncoder encoder,
            Encoding encoding, SaveGamePath path);

        /// <summary>
        /// Occurs when started saving.
        /// </summary>
        public static event SaveHandler OnSaving = delegate { };

        /// <summary>
        /// Occurs when on saved.
        /// </summary>
        public static event SaveHandler OnSaved = delegate { };

        /// <summary>
        /// Occurs when started loading.
        /// </summary>
        public static event LoadHandler OnLoading = delegate { };

        /// <summary>
        /// Occurs when on loaded.
        /// </summary>
        public static event LoadHandler OnLoaded = delegate { };

        /// <summary>
        /// The save callback.
        /// </summary>
        public static SaveHandler SaveCallback = delegate { };

        /// <summary>
        /// The load callback.
        /// </summary>
        public static LoadHandler LoadCallback = delegate { };

        private static ISaveGameSerializer m_Serializer = new SaveGameJsonSerializer();
        private static ISaveGameEncoder m_Encoder = new SaveGameSimpleEncoder();
        private static Encoding m_Encoding = Encoding.UTF8;
        private static bool m_Encode = false;
        private static SaveGamePath m_SavePath = SaveGamePath.PersistentDataPath;
        private static string m_EncodePassword = GenerateDefaultPassword();
        private static bool m_LogError = false;
        private static bool usePlayerPrefs = false;
        private static List<string> ignoredFiles = new List<string>()
        {
            "Player.log",
            "output_log.txt"
        };
        private static List<string> ignoredDirectories = new List<string>()
        {
            "Analytics"
        };

        /// <summary>
        /// Gets or sets the serializer.
        /// </summary>
        /// <value>The serializer.</value>
        public static ISaveGameSerializer Serializer
        {
            get
            {
                m_Serializer ??= new SaveGameJsonSerializer();
                return m_Serializer;
            }
            set
            {
                m_Serializer = value;
            }
        }

        /// <summary>
        /// Gets or sets the encoder.
        /// </summary>
        /// <value>The encoder.</value>
        public static ISaveGameEncoder Encoder
        {
            get
            {
                m_Encoder ??= new SaveGameSimpleEncoder();
                return m_Encoder;
            }
            set
            {
                m_Encoder = value;
            }
        }

        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        /// <value>The encoding.</value>
        public static Encoding DefaultEncoding
        {
            get
            {
                m_Encoding ??= Encoding.UTF8;
                return m_Encoding;
            }
            set
            {
                m_Encoding = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SaveGameFree.SaveGame"/> encrypt data by default.
        /// </summary>
        /// <value><c>true</c> if encode; otherwise, <c>false</c>.</value>
        public static bool Encode
        {
            get
            {
                return m_Encode;
            }
            set
            {
                m_Encode = value;
            }
        }

        /// <summary>
        /// Gets or sets the save path.
        /// </summary>
        /// <value>The save path.</value>
        public static SaveGamePath SavePath
        {
            get
            {
                return m_SavePath;
            }
            set
            {
                m_SavePath = value;
            }
        }

        /// <summary>
        /// Gets or sets the encryption password.
        /// IMPORTANT: For production use, always set your own custom password instead of using the default.
        /// The default password is generated per-device for basic protection but is not cryptographically secure.
        /// </summary>
        /// <value>The encryption password.</value>
        public static string EncodePassword
        {
            get
            {
                return m_EncodePassword;
            }
            set
            {
                m_EncodePassword = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SaveGameFree.SaveGame"/> log error.
        /// </summary>
        /// <value><c>true</c> if log error; otherwise, <c>false</c>.</value>
        public static bool LogError
        {
            get
            {
                return m_LogError;
            }
            set
            {
                m_LogError = value;
            }
        }

        /// <summary>
        /// Gets or sets whether to use PlayerPrefs as storage or not.
        /// </summary>
        public static bool UsePlayerPrefs
        {
            get
            {
                return usePlayerPrefs;
            }
            set
            {
                usePlayerPrefs = value;
            }
        }

        /// <summary>
        /// Gets the list of ignored files.
        /// </summary>
        public static List<string> IgnoredFiles
        {
            get
            {
                return ignoredFiles;
            }
        }

        /// <summary>
        /// Gets the list of ignored directories.
        /// </summary>
        public static List<string> IgnoredDirectories
        {
            get
            {
                return ignoredDirectories;
            }
        }

        /// <summary>
        /// Saves data using the identifier.
        /// </summary>
        public static void Save<T>(string identifier, T objToSave)
        {
            Save<T>(identifier, objToSave, Encode, EncodePassword, Serializer, Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Save the specified identifier, obj, encode and encodePassword.
        /// </summary>
        public static void Save<T>(string identifier, T objToSave, bool encode)
        {
            Save<T>(identifier, objToSave, encode, EncodePassword, Serializer, Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Save the specified identifier, obj and encodePassword.
        /// </summary>
        public static void Save<T>(string identifier, T objToSave, string encodePassword)
        {
            Save<T>(identifier, objToSave, Encode, encodePassword, Serializer, Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Save the specified identifier, obj and serializer.
        /// </summary>
        public static void Save<T>(string identifier, T objToSave, ISaveGameSerializer serializer)
        {
            Save<T>(identifier, objToSave, Encode, EncodePassword, serializer, Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Save the specified identifier, obj and encoder.
        /// </summary>
        public static void Save<T>(string identifier, T objToSave, ISaveGameEncoder encoder)
        {
            Save<T>(identifier, objToSave, Encode, EncodePassword, Serializer, encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Save the specified identifier, obj and encoding.
        /// </summary>
        public static void Save<T>(string identifier, T objToSave, Encoding encoding)
        {
            Save<T>(identifier, objToSave, Encode, EncodePassword, Serializer, Encoder, encoding, SavePath);
        }

        /// <summary>
        /// Save the specified identifier, obj and savePath.
        /// </summary>
        public static void Save<T>(string identifier, T obj, SaveGamePath savePath)
        {
            Save<T>(identifier, obj, Encode, EncodePassword, Serializer, Encoder, DefaultEncoding, savePath);
        }

        /// <summary>
        /// Saves data using the identifier.
        /// </summary>
        public static void Save<T>(string identifier, T objToSave, bool shouldDataBeEncrypted, string encryptionPassword,
            ISaveGameSerializer serializer, ISaveGameEncoder encoder, Encoding encoding, SaveGamePath basePath)
        {
            ValidateIdentifier(identifier);

            try
            {
                OnSaving?.Invoke(objToSave, identifier, shouldDataBeEncrypted, encryptionPassword, serializer,
                    encoder, encoding, basePath);

                serializer ??= Serializer;
                encoding ??= DefaultEncoding;

                string filePath = DecideFilePath(identifier, basePath);

                objToSave ??= default;

                if (shouldDataBeEncrypted)
                {
                    using (var stream = new MemoryStream())
                    {
                        serializer.Serialize(objToSave, stream, encoding);
                        string data = Convert.ToBase64String(stream.ToArray());
                        string encoded = encoder.Encode(data, encryptionPassword);

                        if (!usePlayerPrefs)
                        {
                            File.WriteAllText(filePath, encoded, encoding);
                        }
                        else
                        {
                            PlayerPrefs.SetString(filePath, encoded);
                            PlayerPrefs.Save();
                        }
                    }
                }
                else
                {
                    if (!usePlayerPrefs)
                    {
                        EnsureDirectoryExists(filePath);
                        using (var stream = File.Create(filePath))
                        {
                            serializer.Serialize(objToSave, stream, encoding);
                        }
                    }
                    else
                    {
                        using (var stream = new MemoryStream())
                        {
                            serializer.Serialize(objToSave, stream, encoding);
                            string data = encoding.GetString(stream.ToArray());
                            PlayerPrefs.SetString(filePath, data);
                            PlayerPrefs.Save();
                        }
                    }
                }

                SaveCallback?.Invoke(objToSave, identifier, shouldDataBeEncrypted, encryptionPassword, serializer, encoder, encoding, basePath);
                OnSaved?.Invoke(objToSave, identifier, shouldDataBeEncrypted, encryptionPassword, serializer, encoder, encoding, basePath);
            }
            catch (Exception ex)
            {
                string errorMsg = $"Failed to save data with identifier '{identifier}': {ex.Message}";
                if (LogError)
                {
                    Debug.LogError(errorMsg);
                    Debug.LogException(ex);
                }
                throw new InvalidOperationException(errorMsg, ex);
            }
        }

        private static void ValidateIdentifier(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new System.ArgumentNullException("identifier");
            }
        }

        private static void EnsureDirectoryExists(string filePath)
        {
            // We assume that the path passed is valid
            if (!Directory.Exists(filePath))
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }

        /// <summary>
        /// Loads data using identifier.
        /// </summary>
        public static T Load<T>(string identifier)
        {
            return Load<T>(identifier, default(T), Encode, EncodePassword, Serializer,
                Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Load the specified identifier and defaultValue.
        /// </summary>
        public static T Load<T>(string identifier, T defaultValue)
        {
            return Load<T>(identifier, defaultValue, Encode, EncodePassword, Serializer,
                Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Load the specified identifier and encodePassword.
        /// </summary>
        public static T Load<T>(string identifier, bool encode, string encryptionPassword)
        {
            return Load<T>(identifier, default(T), encode, encryptionPassword, Serializer,
                Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Load the specified identifier and serializer.
        /// </summary>
        public static T Load<T>(string identifier, ISaveGameSerializer serializer)
        {
            return Load<T>(identifier, default(T), Encode, EncodePassword, serializer,
                Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Load the specified identifier and encoder.
        /// </summary>
        public static T Load<T>(string identifier, ISaveGameEncoder encoder)
        {
            return Load<T>(identifier, default(T), Encode, EncodePassword, Serializer,
                encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Load the specified identifier and encoding.
        /// </summary>
        public static T Load<T>(string identifier, Encoding encoding)
        {
            return Load<T>(identifier, default(T), Encode, EncodePassword, Serializer,
                Encoder, encoding, SavePath);
        }

        /// <summary>
        /// Load the specified identifier and savePath.
        /// </summary>
        public static T Load<T>(string identifier, SaveGamePath basePath)
        {
            return Load<T>(identifier, default(T), Encode, EncodePassword, Serializer,
                Encoder, DefaultEncoding, basePath);
        }

        /// <summary>
        /// Load the specified identifier, defaultValue and encode.
        /// Set shouldLoadEncryptedData to true if you have used encryption in save.
        /// </summary>
        public static T Load<T>(string identifier, T defaultValue, bool shouldLoadEncryptedData)
        {
            return Load<T>(identifier, defaultValue, shouldLoadEncryptedData, EncodePassword, Serializer,
                Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Load the specified identifier, defaultValue and encodePassword.
        /// </summary>
        public static T Load<T>(string identifier, T defaultValue, string encryptionPassword)
        {
            return Load<T>(identifier, defaultValue, Encode, encryptionPassword, Serializer,
                Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Load the specified identifier, defaultValue and serializer.
        /// </summary>
        public static T Load<T>(string identifier, T defaultValue, ISaveGameSerializer serializer)
        {
            return Load<T>(identifier, defaultValue, Encode, EncodePassword, serializer,
                Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Load the specified identifier, defaultValue and encoder.
        /// </summary>
        public static T Load<T>(string identifier, T defaultValue, ISaveGameEncoder encoder)
        {
            return Load<T>(identifier, defaultValue, Encode, EncodePassword, Serializer,
                encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Load the specified identifier, defaultValue and encoding.
        /// </summary>
        public static T Load<T>(string identifier, T defaultValue, Encoding encoding)
        {
            return Load<T>(identifier, defaultValue, Encode, EncodePassword, Serializer,
                Encoder, encoding, SavePath);
        }

        /// <summary>
        /// Load the specified identifier, defaultValue and savePath.
        /// </summary>
        public static T Load<T>(string identifier, T defaultValue, SaveGamePath savePath)
        {
            return Load<T>(identifier, defaultValue, Encode, EncodePassword, Serializer,
                Encoder, DefaultEncoding, savePath);
        }

        /// <summary>
        /// Loads data using identifier.
        /// Set shouldLoadEncryptedData to true if you have used encryption in save.
        /// </summary>
        public static T Load<T>(string identifier, T defaultValue, bool shouldLoadEncryptedData, string encryptionPassword,
            ISaveGameSerializer serializer, ISaveGameEncoder encoder, Encoding encoding, SaveGamePath basePath)
        {
            ValidateIdentifier(identifier);

            try
            {
                OnLoading?.Invoke(null, identifier, shouldLoadEncryptedData, encryptionPassword, serializer, encoder, encoding, basePath);
                serializer ??= SaveGame.Serializer;
                encoding ??= SaveGame.DefaultEncoding;
                defaultValue ??= default;
                T result = defaultValue;
                string filePath = DecideFilePath(identifier, basePath);
                if (!Exists(filePath, basePath))
                {
                    Debug.LogWarning(
                        $"The specified identifier({identifier}) does not exist. Please use Exists() to check before calling Load.\n" +
                        "Returning the default(T) instance.");
                    return result;
                }

                if (shouldLoadEncryptedData)
                {
                    string data = usePlayerPrefs
                        ? PlayerPrefs.GetString(filePath)
                        : File.ReadAllText(filePath, encoding);

                    string decoded = encoder.Decode(data, encryptionPassword);
                    using (var stream = new MemoryStream(Convert.FromBase64String(decoded), false))
                    {
                        result = serializer.Deserialize<T>(stream, encoding);
                    }
                }
                else
                {
                    if (!usePlayerPrefs)
                    {
                        using (var stream = File.OpenRead(filePath))
                        {
                            result = serializer.Deserialize<T>(stream, encoding);
                        }
                    }
                    else
                    {
                        string data = PlayerPrefs.GetString(filePath);
                        using (var stream = new MemoryStream(encoding.GetBytes(data)))
                        {
                            result = serializer.Deserialize<T>(stream, encoding);
                        }
                    }
                }

                result ??= defaultValue;
                LoadCallback?.Invoke(result, identifier, shouldLoadEncryptedData, encryptionPassword, serializer,
                    encoder, encoding, basePath);

                OnLoaded?.Invoke(result, identifier, shouldLoadEncryptedData, encryptionPassword, serializer,
                    encoder, encoding, basePath);

                return result;
            }
            catch (Exception ex)
            {
                string errorMsg = $"Failed to load data with identifier '{identifier}': {ex.Message}";
                if (LogError)
                {
                    Debug.LogError(errorMsg);
                    Debug.LogException(ex);
                }
                throw new InvalidOperationException(errorMsg, ex);
            }
        }

        /// <summary>
        /// Saves data asynchronously using the identifier.
        /// </summary>
        public static async System.Threading.Tasks.Task SaveAsync<T>(string identifier, T objToSave)
        {
            await SaveAsync<T>(identifier, objToSave, Encode, EncodePassword, Serializer, Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Saves data asynchronously using the identifier with encryption.
        /// </summary>
        public static async System.Threading.Tasks.Task SaveAsync<T>(string identifier, T objToSave, bool encode)
        {
            await SaveAsync<T>(identifier, objToSave, encode, EncodePassword, Serializer, Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Saves data asynchronously using the identifier with full options.
        /// </summary>
        public static async System.Threading.Tasks.Task SaveAsync<T>(string identifier, T objToSave, bool shouldDataBeEncrypted, string encryptionPassword,
            ISaveGameSerializer serializer, ISaveGameEncoder encoder, Encoding encoding, SaveGamePath basePath)
        {
            ValidateIdentifier(identifier);

            try
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    OnSaving?.Invoke(objToSave, identifier, shouldDataBeEncrypted, encryptionPassword, serializer,
                        encoder, encoding, basePath);
                });

                serializer ??= Serializer;
                encoding ??= DefaultEncoding;

                string filePath = DecideFilePath(identifier, basePath);

                objToSave ??= default;

                if (shouldDataBeEncrypted)
                {
                    using (var stream = new MemoryStream())
                    {
                        serializer.Serialize(objToSave, stream, encoding);
                        string data = Convert.ToBase64String(stream.ToArray());
                        string encoded = encoder.Encode(data, encryptionPassword);

                        if (!usePlayerPrefs)
                        {
                            await System.IO.File.WriteAllTextAsync(filePath, encoded, encoding);
                        }
                        else
                        {
                            PlayerPrefs.SetString(filePath, encoded);
                            PlayerPrefs.Save();
                        }
                    }
                }
                else
                {
                    if (!usePlayerPrefs)
                    {
                        EnsureDirectoryExists(filePath);
                        using (var stream = File.Create(filePath))
                        {
                            serializer.Serialize(objToSave, stream, encoding);
                            await stream.FlushAsync();
                        }
                    }
                    else
                    {
                        using (var stream = new MemoryStream())
                        {
                            serializer.Serialize(objToSave, stream, encoding);
                            string data = encoding.GetString(stream.ToArray());
                            PlayerPrefs.SetString(filePath, data);
                            PlayerPrefs.Save();
                        }
                    }
                }

                await System.Threading.Tasks.Task.Run(() =>
                {
                    SaveCallback?.Invoke(objToSave, identifier, shouldDataBeEncrypted, encryptionPassword, serializer, encoder, encoding, basePath);
                    OnSaved?.Invoke(objToSave, identifier, shouldDataBeEncrypted, encryptionPassword, serializer, encoder, encoding, basePath);
                });
            }
            catch (Exception ex)
            {
                string errorMsg = $"Failed to save data asynchronously with identifier '{identifier}': {ex.Message}";
                if (LogError)
                {
                    Debug.LogError(errorMsg);
                    Debug.LogException(ex);
                }
                throw new InvalidOperationException(errorMsg, ex);
            }
        }

        /// <summary>
        /// Loads data asynchronously using identifier.
        /// </summary>
        public static async System.Threading.Tasks.Task<T> LoadAsync<T>(string identifier)
        {
            return await LoadAsync<T>(identifier, default(T), Encode, EncodePassword, Serializer,
                Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Loads data asynchronously using identifier with default value.
        /// </summary>
        public static async System.Threading.Tasks.Task<T> LoadAsync<T>(string identifier, T defaultValue)
        {
            return await LoadAsync<T>(identifier, defaultValue, Encode, EncodePassword, Serializer,
                Encoder, DefaultEncoding, SavePath);
        }

        /// <summary>
        /// Loads data asynchronously using identifier with full options.
        /// </summary>
        public static async System.Threading.Tasks.Task<T> LoadAsync<T>(string identifier, T defaultValue, bool shouldLoadEncryptedData, string encryptionPassword,
            ISaveGameSerializer serializer, ISaveGameEncoder encoder, Encoding encoding, SaveGamePath basePath)
        {
            ValidateIdentifier(identifier);

            try
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    OnLoading?.Invoke(null, identifier, shouldLoadEncryptedData, encryptionPassword, serializer, encoder, encoding, basePath);
                });

                serializer ??= SaveGame.Serializer;
                encoding ??= SaveGame.DefaultEncoding;
                defaultValue ??= default;
                T result = defaultValue;
                string filePath = DecideFilePath(identifier, basePath);

                if (!Exists(filePath, basePath))
                {
                    Debug.LogWarning(
                        $"The specified identifier({identifier}) does not exist. Please use Exists() to check before calling LoadAsync.\n" +
                        "Returning the default(T) instance.");
                    return result;
                }

                if (shouldLoadEncryptedData)
                {
                    string data = usePlayerPrefs
                        ? PlayerPrefs.GetString(filePath)
                        : await File.ReadAllTextAsync(filePath, encoding);

                    string decoded = encoder.Decode(data, encryptionPassword);
                    using (var stream = new MemoryStream(Convert.FromBase64String(decoded), false))
                    {
                        result = serializer.Deserialize<T>(stream, encoding);
                    }
                }
                else
                {
                    if (!usePlayerPrefs)
                    {
                        using (var stream = File.OpenRead(filePath))
                        {
                            result = serializer.Deserialize<T>(stream, encoding);
                        }
                    }
                    else
                    {
                        string data = PlayerPrefs.GetString(filePath);
                        using (var stream = new MemoryStream(encoding.GetBytes(data)))
                        {
                            result = serializer.Deserialize<T>(stream, encoding);
                        }
                    }
                }

                result ??= defaultValue;

                await System.Threading.Tasks.Task.Run(() =>
                {
                    LoadCallback?.Invoke(result, identifier, shouldLoadEncryptedData, encryptionPassword, serializer,
                        encoder, encoding, basePath);

                    OnLoaded?.Invoke(result, identifier, shouldLoadEncryptedData, encryptionPassword, serializer,
                        encoder, encoding, basePath);
                });

                return result;
            }
            catch (Exception ex)
            {
                string errorMsg = $"Failed to load data asynchronously with identifier '{identifier}': {ex.Message}";
                if (LogError)
                {
                    Debug.LogError(errorMsg);
                    Debug.LogException(ex);
                }
                throw new InvalidOperationException(errorMsg, ex);
            }
        }

        /// <summary>
        /// Checks whether the specified identifier exists or not.
        /// </summary>
        public static bool Exists(string identifier)
        {
            return Exists(identifier, SavePath);
        }

        /// <summary>
        /// Checks whether the specified identifier exists or not.
        /// </summary>
        public static bool Exists(string identifier, SaveGamePath basePath)
        {
            ValidateIdentifier(identifier);
            string filePath = DecideFilePath(identifier, basePath);

            if (!usePlayerPrefs)
            {
                bool exists = Directory.Exists(filePath);
                if (!exists)
                {
                    exists = File.Exists(filePath);
                }
                return exists;
            }
            else
            {
                return PlayerPrefs.HasKey(filePath);
            }
        }

        /// <summary>
        /// Delete the specified identifier.
        /// </summary>
        public static void Delete(string identifier)
        {
            Delete(identifier, SavePath);
        }

        /// <summary>
        /// Delete the specified identifier and path.
        /// </summary>
        public static void Delete(string identifier, SaveGamePath basePath)
        {
            ValidateIdentifier(identifier);
            string filePath = DecideFilePath(identifier, basePath);

            if (!Exists(filePath, basePath))
            {
                return;
            }
            if (!usePlayerPrefs)
            {
                var fileName = Path.GetFileName(filePath);
                if (ignoredFiles.Contains(fileName) || ignoredDirectories.Contains(fileName))
                {
                    return;
                }
                if (File.Exists(filePath))
                    File.Delete(filePath);
                else if (Directory.Exists(filePath))
                    Directory.Delete(filePath, true);
            }
            else
            {
                PlayerPrefs.DeleteKey(filePath);
            }
        }

        /// <summary>
        /// Clear this instance.
        /// Alias of DeleteAll.
        /// </summary>
        public static void Clear()
        {
            DeleteAll(SavePath);
        }

        /// <summary>
        /// Clear the specified base path.
        /// Alias of DeleteAll.
        /// </summary>
        public static void Clear(SaveGamePath basePath)
        {
            DeleteAll(basePath);
        }

        /// <summary>
        /// Deletes all.
        /// </summary>
        public static void DeleteAll()
        {
            DeleteAll(SavePath);
        }

        /// <summary>
        /// Deletes all.
        /// </summary>
        public static void DeleteAll(SaveGamePath path)
        {
            string dirPath = "";
            switch (path)
            {
                case SaveGamePath.PersistentDataPath:
                    dirPath = Application.persistentDataPath;
                    break;
                case SaveGamePath.DataPath:
                    dirPath = Application.dataPath;
                    break;
            }

            if (!usePlayerPrefs)
            {
                DirectoryInfo info = new DirectoryInfo(dirPath);
                FileInfo[] files = info.GetFiles();
                for(int i = 0; i < files.Length; i++)
                {
                    if (ignoredFiles.Contains(files[i].Name))
                    {
                        continue;
                    }
                    files[i].Delete();
                }
                DirectoryInfo[] dirs = info.GetDirectories();
                for(int i = 0; i < dirs.Length; i++)
                {
                    if (ignoredDirectories.Contains(dirs[i].Name))
                    {
                        continue;
                    }
                    dirs[i].Delete(true);
                }
            }
            else
            {
                PlayerPrefs.DeleteAll();
            }
        }

        /// <summary>
        /// Retrieves files from the save path home.
        /// </summary>
        public static FileInfo[] GetFiles()
        {
            return GetFiles(string.Empty, SavePath);
        }

        /// <summary>
        /// Retrieves files from the given directory path.
        /// </summary>
        public static FileInfo[] GetFiles(string identifier)
        {
            return GetFiles(identifier, SavePath);
        }

        /// <summary>
        /// Retrieves files from the given directory path.
        /// </summary>
        public static FileInfo[] GetFiles(string identifier, SaveGamePath path)
        {
            identifier ??= string.Empty;

            string filePath = DecideFilePath(identifier, path);

            FileInfo[] files = new FileInfo[0];
            if (!Exists(filePath, path))
            {
                return files;
            }
            if (Directory.Exists(filePath))
            {
                DirectoryInfo info = new DirectoryInfo(filePath);
                files = info.GetFiles();
            }
            return files;
        }

        /// <summary>
        /// Retrieves directories from the save path home.
        /// </summary>
        public static IList<DirectoryInfo> GetDirectories()
        {
            return GetDirectories(string.Empty, SavePath);
        }

        /// <summary>
        /// Retrieves directories from the given directory path.
        /// </summary>
        public static IList<DirectoryInfo> GetDirectories(string identifier)
        {
            return GetDirectories(identifier, SavePath);
        }

        /// <summary>
        /// Retrieves directories from the given directory path.
        /// </summary>
        public static IList<DirectoryInfo> GetDirectories(string identifier, SaveGamePath path)
        {
            identifier ??= string.Empty;
            string filePath = DecideFilePath(identifier, path);

            IList<DirectoryInfo> directories;
            if (!Exists(filePath, path))
            {
                directories = Array.Empty<DirectoryInfo>();
            }
            else
            {
                directories = DecideDirectoryInfo(filePath, path);
            }
            return directories;
        }

        private static string DecideFilePath(string identifier, SaveGamePath basePath)
        {
            string result;
            if (!IsFilePath(identifier) && basePath != SaveGamePath.Custom)
            {
                // In cases like this, we expect the identifier to belong to an instance of a data structure
                // of what represents the state of a whole playthrough, as opposed to a mere Vector3 position
                // or small custom class that is supposed to be part of a bigger save-data class.
                // And since this system doesn't specify slots, we assume that the identifier has the 
                // slot number in it.
                switch (basePath)
                {
                    default:
                    case SaveGamePath.PersistentDataPath:
                        result = $"{Application.persistentDataPath}/{identifier}";
                        break;
                    case SaveGamePath.DataPath:
                        result = $"{Application.dataPath}/{identifier}";
                        break;
                }
            }
            else
            {
                result = identifier;
            }

            return result;
        }

        private static IList<DirectoryInfo> DecideDirectoryInfo(string filePath, SaveGamePath basePath)
        {
            IList<DirectoryInfo> result = new DirectoryInfo[0];
            if (Directory.Exists(filePath))
            {
                DirectoryInfo info = new DirectoryInfo(filePath);
                result = info.GetDirectories();
            }
            return result;
        }


        public static bool IsFilePath(string inputString)
        {
            bool result = false;
#if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
            if (Path.IsPathRooted(inputString))
            {
                try
                {
                    Path.GetFullPath(inputString);
                    result = true;
                }
                catch(System.Exception)
                {
                    result = false;
                }
            }
#endif
            return result;
        }

        /// <summary>
        /// Generates a default password based on device and application identifiers.
        /// This provides a unique password per device/application instead of a hardcoded value.
        /// IMPORTANT: For production use, always set your own custom password using EncodePassword property.
        /// </summary>
        private static string GenerateDefaultPassword()
        {
            // Generate a device/application-specific password instead of hardcoded value
            // This is still not secure for production but better than a hardcoded string
            string deviceId = SystemInfo.deviceUniqueIdentifier;
            string appId = Application.identifier;

            // Create a simple hash-like combination
            int hash = (deviceId + appId).GetHashCode();
            return $"SGF_{hash:X8}";
        }

    }

}
