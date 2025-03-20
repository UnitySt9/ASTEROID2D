using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _endScore;

        public void ShowGameOverPanel(int score)
        {
            _gameOverPanel.SetActive(true);
            _endScore.text = $"GAME OVER. SCORE: {score}";
        }
    }
}
