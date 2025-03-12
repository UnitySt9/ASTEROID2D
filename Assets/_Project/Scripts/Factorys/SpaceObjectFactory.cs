using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    public class SpaceObjectFactory
    {
        public event Action<SpaceObject> OnSpaceObjectCreated;

        private Asteroid _asteroidPrefab;
        private GameStateManager _gameStateManager;

        public SpaceObjectFactory(Asteroid asteroidPrefab, GameStateManager gameStateManager)
        {
            _asteroidPrefab = asteroidPrefab;
            _gameStateManager = gameStateManager;
        }

        public void CreateAsteroid(Vector2 position)
        {
            Asteroid asteroid = Object.Instantiate(_asteroidPrefab, position, Quaternion.identity);
            asteroid.Initialize(_gameStateManager);
            _gameStateManager.RegisterListener(asteroid);
            OnSpaceObjectCreated?.Invoke(asteroid);
        }
    }
}
