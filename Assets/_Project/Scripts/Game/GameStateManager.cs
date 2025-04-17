using System.Collections.Generic;

namespace _Project.Scripts
{
    public class GameStateManager
    {
        private readonly List<IGameStateListener> _listeners = new();
        private readonly IAnalyticsService _analyticsService;
        private bool _gameStarted = false;

        public GameStateManager(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public void GameStart()
        {
            if (!_gameStarted)
            {
                _gameStarted = true;
                _analyticsService.TrackGameStart();
            }
        }

        public void GameOver()
        {
            foreach (var listener in _listeners)
            {
                listener.OnGameOver();
            }
        }
        
        public void ContinueGame()
        {
            foreach (var listener in _listeners)
            {
                listener.OnGameContinue();
            }
            _gameStarted = true;
        }
        
        public void GameOverStats(int shotsFired, int lasersUsed, int objectsDestroyed)
        {
            _analyticsService.TrackGameEnd(shotsFired, lasersUsed, objectsDestroyed);
        }

        public void RegisterListener(IGameStateListener listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(IGameStateListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
