using UnityEngine;

using Bayat.Unity.SaveGameFree;

public class Test : MonoBehaviour
{

    void Start()
    {
        SaveGame.Encode = true;
        SaveGame.EncodePassword = "1234";
        SaveGame.Save<int>("blabla", 1);
        SaveGame.Load<int>("blabla", 1, 0);
    }

}
