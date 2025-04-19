using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class UFOFactory : IDisposable
    {
        public event Action<UFO> OnUFOCreated;
        
        private readonly GameStateManager _gameStateManager;
        private readonly DiContainer _container;
        private readonly IAddressablesLoader _addressablesLoader;
        
        private Transform _spaceShipTransform;
        private GameObject _ufoPrefab;
        private bool _isInitialized;

        [Inject]
        public UFOFactory(
            GameStateManager gameStateManager, 
            DiContainer container,
            IAddressablesLoader addressablesLoader)
        {
            _gameStateManager = gameStateManager;
            _container = container;
            _addressablesLoader = addressablesLoader;
        }

        public async UniTask Initialize(ShipTransform shipTransform)
        {
            if (_isInitialized) return;
            
            _spaceShipTransform = shipTransform.transform;
            _ufoPrefab = await _addressablesLoader.LoadUFOPrefab();
            _isInitialized = true;
        }

        public void CreateUFO(Vector2 position)
        {
            var ufoInstance = _container.InstantiatePrefabForComponent<UFO>(_ufoPrefab, position, Quaternion.identity, null);
            ufoInstance.Initialize(_spaceShipTransform, _gameStateManager);
            _gameStateManager.RegisterListener(ufoInstance);
            OnUFOCreated?.Invoke(ufoInstance);
        }

        public void Dispose()
        {
            _addressablesLoader.ReleaseAsset(_ufoPrefab);
            _ufoPrefab = null;
            _isInitialized = false;
            OnUFOCreated = null;
        }
    }
}
