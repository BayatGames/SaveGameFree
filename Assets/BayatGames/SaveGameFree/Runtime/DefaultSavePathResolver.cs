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
            get => m_relativePath;
            protected set
            {
                string processedPath = value?.Trim()
                    .TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                    .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                if (string.IsNullOrEmpty(processedPath))
                {
                    Debug.LogWarning($"Provided relative path was empty, null, or contained only " +
                        $"slashes. Reverting to default: '{m_defaultRelativePath}'");
                    m_relativePath = m_defaultRelativePath;
                }
                else
                {
                    m_relativePath = processedPath;
                }

            }
        }

        protected string m_relativePath = "Saves";
        protected static readonly string m_defaultRelativePath = "Saves";

        public string FileExtension
        {
            get => m_fileExtension;
            protected set
            {
                var processedExtension = value?.Trim().TrimStart('.');

                if (string.IsNullOrEmpty(processedExtension))
                {
                    Debug.LogWarning($"File extension is empty or null after trimming. Reverting " +
                        $"it to the default: {m_defaultFileExtension}");
                    m_fileExtension = m_defaultFileExtension;
                }
                else
                {
                    m_fileExtension = processedExtension;
                }
            }
        }
        protected string m_fileExtension = "sav";
        protected static readonly string m_defaultFileExtension = "sav";

        public string GetSaveFilePath(string fileName, object input)
        {
            if (input is not SaveGamePath)
            {
                Debug.LogWarning("Input is not of type SaveGamePath. Acting as if it was " +
                    "SaveGamePath.PersistentDataPath.");
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
                Debug.LogWarning("Input is not of type SaveGamePath. Acting as if it was " +
                    "SaveGamePath.PersistentDataPath.");
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
