using System;

namespace _Project.Scripts
{
    public class GameOverUIController : IDisposable, IGameStateListener
    {
        private GameOverView _gameOverView;
        private Score _score;
        private GameStateManager _gameStateManager;
        
        public void Initialize(GameOverView gameOverView, Score score, GameStateManager gameStateManager)
        {
            _gameOverView = gameOverView;
            _score = score;
            _gameStateManager = gameStateManager;
            _gameStateManager.RegisterListener(this);
        }

        public void Dispose()
        {
            if (_gameStateManager != null)
            {
                _gameStateManager.UnregisterListener(this);
            }
        }
        
        public void OnGameOver()
        {
            _gameOverView.ShowGameOverPanel(_score.Count);
        }
    }
}

