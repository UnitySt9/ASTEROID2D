using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _endScore;
        [SerializeField] private TextMeshProUGUI _highScore;
        [SerializeField] private GameObject _newRecordLabel;

        public void ShowPanel(int score, int highScore, bool isNewRecord)
        {
            _gameOverPanel.SetActive(true);
            _endScore.text = $"GAME OVER. SCORE: {score}";
            _highScore.text = $"BEST SCORE: {highScore}";
            _newRecordLabel.SetActive(isNewRecord);
        }
    }
}
