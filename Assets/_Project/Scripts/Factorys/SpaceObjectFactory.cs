using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class SpaceObjectFactory : MonoBehaviour
    {
        public event Action<SpaceObject> OnSpaceObjectCreated;
        
        [SerializeField] Asteroid _asteroid;
        private GameStateManager _gameStateManager;

        public void CreateAsteroid(Vector2 position, GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
            Asteroid asteroid = Instantiate(_asteroid, position, Quaternion.identity);
            asteroid.SetDependency(_gameStateManager);
            OnSpaceObjectCreated?.Invoke(asteroid);
        }
    }
}
