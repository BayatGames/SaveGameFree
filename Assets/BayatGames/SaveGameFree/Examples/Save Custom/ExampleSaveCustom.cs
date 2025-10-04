using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGameFree.Examples
{

    public class ExampleSaveCustom : MonoBehaviour
    {
        [SerializeField] protected SaveGamePath saveGamePath = SaveGamePath.PersistentDataPath;

        [System.Serializable]
        public struct Level
        {
            public bool unlocked;
            public bool completed;

            public Level(bool unlocked, bool completed)
            {
                this.unlocked = unlocked;
                this.completed = completed;
            }
        }

        [System.Serializable]
        public class CustomData
        {

            public int score;
            public int highScore;
            public List<Level> levels;

            public CustomData()
            {
                this.score = 0;
                this.highScore = 0;

                // Dummy data
                this.levels = new List<Level>() {
                    new Level(true, false),
                    new Level(false, false),
                    new Level(false, true),
                    new Level(true, false)
                };
            }

        }

        public CustomData customData;
        public bool loadOnStart = true;
        public InputField scoreInputField;
        public InputField highScoreInputField;
        public string identifier = "exampleSaveCustom";

        void Start()
        {
            if (this.loadOnStart)
            {
                Load();
            }
        }

        public void SetScore(string score)
        {
            this.customData.score = int.Parse(score);
        }

        public void SetHighScore(string highScore)
        {
            this.customData.highScore = int.Parse(highScore);
        }

        public void Save()
        {
            SaveGame.SavePath = saveGamePath;
            SaveGame.Save<CustomData>(this.identifier, this.customData, SerializerDropdown.Singleton.ActiveSerializer);
        }

        public void Load()
        {
            this.customData = SaveGame.Load<CustomData>(
                this.identifier,
                new CustomData(),
                SerializerDropdown.Singleton.ActiveSerializer);
            this.scoreInputField.text = this.customData.score.ToString();
            this.highScoreInputField.text = this.customData.highScore.ToString();
        }

        public void Delete()
        {
            SaveGame.Delete(this.identifier);
        }

        public void DeleteAll()
        {
            SaveGame.DeleteAll();
        }

    }

}