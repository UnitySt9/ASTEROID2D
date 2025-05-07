using System;

namespace _Project.Scripts
{
    [Serializable]
    public class GameData
    {
        public int HighScore;
        public bool AdsDisabled;
        public DateTime SaveDateTime;
        
        public GameData()
        {
            SaveDateTime = DateTime.MinValue;
        }
    }
}
