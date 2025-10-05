using UnityEngine;
using System.IO;

namespace Bayat.Unity.SaveGameFree
{
    public class DefaultSavePathResolver : ISavePathResolver, ISavePathResolver<SaveGamePath>
    {
        public DefaultSavePathResolver(string relativePath = "Saves", string fileExtension = "sav")
        {
            RelativePath = relativePath;
            FileExtension = fileExtension;
        }

        public virtual string RelativePath
        {
            get => _relativePath;
            protected set
            {
                _relativePath = value;

                // We want to make sure that the relative path is always valid
                if (string.IsNullOrEmpty(_relativePath))
                {
                    Debug.LogWarning($"Relative path is empty or null. Reverting it to the default: {_defaultRelativePath}");
                    _relativePath = _defaultRelativePath;
                }
                else
                {
                    // We don't want to allow any slashes at the start or end of the path
                    _relativePath = value.Trim()
                        .TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                        .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                    if (string.IsNullOrEmpty(_relativePath))
                    {
                        Debug.LogWarning($"Relative path is empty or null after trimming slashes. Reverting it to the default: {_defaultRelativePath}");
                        _relativePath = _defaultRelativePath;
                    }
                }
                
            }
        }

        protected string _relativePath = "Saves";
        protected static readonly string _defaultRelativePath = "Saves";

        public string FileExtension
        {
            get => _fileExtension;
            protected set
            {
                _fileExtension = value;
                if (string.IsNullOrEmpty(_fileExtension))
                {
                    Debug.LogWarning($"File extension is empty or null. Reverting it " +
                        $"to the default: {_defaultFileExtension}");
                    _fileExtension = _defaultFileExtension;
                }
                else
                {
                    _fileExtension = value.Trim().TrimStart('.'); // We want to insert the dot ourselves
                    if (string.IsNullOrEmpty(_fileExtension))
                    {
                        Debug.LogWarning($"File extension is empty or null after trimming. Reverting it " +
                            $"to the default: {_defaultFileExtension}");
                        _fileExtension = _defaultFileExtension;
                    }
                }
            }
        }
        protected string _fileExtension = "sav";
        protected static readonly string _defaultFileExtension = "sav";

        public string GetSaveFilePath(string fileName, object input)
        {
            if (input is not SaveGamePath)
            {
                Debug.LogWarning("Input is not of type SaveGamePath. Acting as if it was SaveGamePath.PersistentDataPath.");
                input = SaveGamePath.PersistentDataPath;
            }

            return GetSaveFilePath(fileName, (SaveGamePath)input);
        }

        public string GetSaveFilePath(string fileName, SaveGamePath input)
        {
            string folderPath = GetSaveFolderPath(input);
            string fileNameWithExt = $"{fileName}.{FileExtension}";
            string result = Path.Join(folderPath, fileNameWithExt);
            return result;
        }

        public string GetSaveFolderPath(object input)
        {
            string result;
            if (input is not SaveGamePath)
            {
                Debug.LogWarning("Input is not of type SaveGamePath. Acting as if it was SaveGamePath.PersistentDataPath.");
                input = SaveGamePath.PersistentDataPath;
            }

            result = GetSaveFolderPath((SaveGamePath)input);
            return result;
        }

        public string GetSaveFolderPath(SaveGamePath pathEnum)
        {
            string result;
            switch (pathEnum)
            {
                case SaveGamePath.PersistentDataPath:
                    result = Application.persistentDataPath; break;
                case SaveGamePath.DataPath:
                    result = Application.dataPath; break;
                default:
                    result = Application.persistentDataPath; break;
            }

            result = Path.Join(result, RelativePath);
            return result;
        }

        
    }
}
