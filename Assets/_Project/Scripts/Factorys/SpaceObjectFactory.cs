using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class SpaceObjectFactory
    {
        public event Action<SpaceObject> OnSpaceObjectCreated;
        
        private GameStateManager _gameStateManager;
        private DiContainer _container;
        private IAddressablesLoader _addressablesLoader;

        [Inject]
        public SpaceObjectFactory(
            GameStateManager gameStateManager, 
            DiContainer container,
            IAddressablesLoader addressablesLoader)
        {
            _gameStateManager = gameStateManager;
            _container = container;
            _addressablesLoader = addressablesLoader;
        }

        public async UniTask CreateAsteroid(Vector2 position)
        {
            var asteroidPrefab = await _addressablesLoader.LoadAsteroidPrefab();
            Asteroid asteroid = _container.InstantiatePrefabForComponent<Asteroid>(asteroidPrefab, position, Quaternion.identity, null);
            asteroid.SetLoadedPrefab(asteroidPrefab);
            asteroid.Initialize(_gameStateManager);
            _gameStateManager.RegisterListener(asteroid);
            OnSpaceObjectCreated?.Invoke(asteroid);
        }
    }
}
