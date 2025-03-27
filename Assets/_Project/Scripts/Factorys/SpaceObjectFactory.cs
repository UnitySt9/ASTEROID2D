using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class SpaceObjectFactory
    {
        public event Action<SpaceObject> OnSpaceObjectCreated;

        private Asteroid _asteroidPrefab;
        private GameStateManager _gameStateManager;
        private DiContainer _container;

        [Inject]
        public SpaceObjectFactory(Asteroid asteroidPrefab, GameStateManager gameStateManager, DiContainer container)
        {
            _asteroidPrefab = asteroidPrefab;
            _gameStateManager = gameStateManager;
            _container = container;
        }

        public void CreateAsteroid(Vector2 position)
        {
            Asteroid asteroid = _container.InstantiatePrefabForComponent<Asteroid>(_asteroidPrefab, position, Quaternion.identity, null);
            asteroid.Initialize(_gameStateManager);
            _gameStateManager.RegisterListener(asteroid);
            OnSpaceObjectCreated?.Invoke(asteroid);
        }
    }
}
