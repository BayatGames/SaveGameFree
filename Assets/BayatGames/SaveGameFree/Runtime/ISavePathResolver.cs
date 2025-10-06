using UnityEngine;

namespace Bayat.Unity.SaveGameFree
{
    public interface ISavePathResolver
    {
        string RelativePath { get; }
        string FileExtension { get; }
        string GetSaveFolderPath(object input);
        string GetSaveFilePath(string fileName, object input);
    }

    public interface ISavePathResolver<TInput> : ISavePathResolver
    {
        string GetSaveFolderPath(TInput input);
        string GetSaveFilePath(string fileName, TInput input);
    }
}
