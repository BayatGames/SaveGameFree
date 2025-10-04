using UnityEngine;

namespace BayatGames.SaveGameFree
{
    /// <summary>
    /// Save game path. base paths for your save games.
    /// </summary>
    public enum SaveGamePath
    {

        /// <summary>
        /// The persistent data path. Application.persistentDataPath
        /// </summary>
        PersistentDataPath,

        /// <summary>
        /// The data path. Application.dataPath
        /// </summary>
        DataPath,

        /// <summary>
        /// The custom path
        /// </summary>
        Custom

    }

}