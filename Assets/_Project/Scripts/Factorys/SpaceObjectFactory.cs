using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class SpaceObjectFactory: IDisposable
    {
        public event Action<SpaceObject> OnSpaceObjectCreated;
        
        private GameObject _asteroidPrefab;
        private GameStateManager _gameStateManager;
        private DiContainer _container;
        private IAddressablesLoader _addressablesLoader;
        private bool _isInitialized;

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

        public async UniTask Initialize()
        {
            if (_isInitialized) return;
            _asteroidPrefab = await _addressablesLoader.LoadAsteroidPrefab();
            _isInitialized = true;
        }
        
        public void CreateAsteroid(Vector2 position)
        {
            Asteroid asteroid = _container.InstantiatePrefabForComponent<Asteroid>(_asteroidPrefab, position, Quaternion.identity, null);
            asteroid.Initialize(_gameStateManager);
            _gameStateManager.RegisterListener(asteroid);
            OnSpaceObjectCreated?.Invoke(asteroid);
        }
        
        public void Dispose()
        {
            _addressablesLoader.ReleaseAsset(_asteroidPrefab);
            _asteroidPrefab = null;
            _isInitialized = false;
            OnSpaceObjectCreated = null;
        }
    }
}
