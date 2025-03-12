using UnityEngine;

namespace _Project.Scripts
{
    public class CollisionHandler : MonoBehaviour
    {
        private GameStateManager _gameStateManager;

        public void Initialize(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        private void OnCollisionEnter2D()
        {
            _gameStateManager?.GameOver();
        }
    }
}
