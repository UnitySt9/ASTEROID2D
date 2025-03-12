using System.Collections.Generic;

namespace _Project.Scripts
{
    public class GameStateManager
    {
        private List<IGameStateListener> _listeners = new();

        public void RegisterListener(IGameStateListener listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(IGameStateListener listener)
        {
            _listeners.Remove(listener);
        }

        public void GameOver()
        {
            foreach (var listener in _listeners)
            {
                listener.OnGameOver();
            }
        }
    }
}
