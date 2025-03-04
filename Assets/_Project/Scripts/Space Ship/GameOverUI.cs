using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GameOverUI : MonoBehaviour, IGameStateListener
    {
        [SerializeField] GameObject _gameOverPanel;
        [SerializeField] TextMeshProUGUI _endScore;
        [SerializeField] Score _score;
        [SerializeField] GameStateManager _gameStateManager;

        private void Start()
        {
            _gameStateManager.RegisterListener(this);
        }

        private void OnDestroy()
        {
            _gameStateManager.UnregisterListener(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _gameStateManager.GameOver();
        }

        public void OnGameOver()
        {
            _gameOverPanel.SetActive(true);
            _endScore.text = "GAME OVER. SCORE: " + _score.Count;
        }
    }
}

