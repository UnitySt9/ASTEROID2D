using UnityEngine;

namespace _Project.Scripts
{
    public class PlayerPrefsSaveService : ISaveService
    {
        private const string GAME_DATA_KEY = "GameData";

        public GameData Load()
        {
            if (PlayerPrefs.HasKey(GAME_DATA_KEY))
            {
                string json = PlayerPrefs.GetString(GAME_DATA_KEY);
                return JsonUtility.FromJson<GameData>(json);
            }
            return new GameData();
        }

        public void Save(GameData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(GAME_DATA_KEY, json);
            PlayerPrefs.Save();
        }
    }
}
