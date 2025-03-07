using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GameOverUI : MonoBehaviour, IGameStateListener
    {
        private GameObject _gameOverPanel;
        private TextMeshProUGUI _endScore;
        private Score _score;
        private GameStateManager _gameStateManager;

        public void Initialize(GameObject gameOverPanel, TextMeshProUGUI endScore, Score score, GameStateManager gameStateManager)
        {
            _gameOverPanel = gameOverPanel;
            _endScore = endScore;
            _score = score;
            _gameStateManager = gameStateManager;
            _gameStateManager.RegisterListener(this);
        }

        private void OnDestroy()
        {
            if (_gameStateManager != null)
            {
                _gameStateManager.UnregisterListener(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _gameStateManager?.GameOver();
        }

        public void OnGameOver()
        {
            _gameOverPanel.SetActive(true);
            _endScore.text = "GAME OVER. SCORE: " + _score.Count;
        }
    }
}

