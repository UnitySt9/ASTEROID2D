using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class UFOFactory
    {
        public event Action<UFO> OnUFOCreated;
        
        private GameStateManager _gameStateManager;
        private Transform _spaceShipTransform;
        private ShipTransform _shipTransform;
        private DiContainer _container;
        private IAddressablesLoader _addressablesLoader;

        public UFOFactory(
            GameStateManager gameStateManager, 
            DiContainer container,
            IAddressablesLoader addressablesLoader)
        {
            _gameStateManager = gameStateManager;
            _container = container;
            _addressablesLoader = addressablesLoader;
        }

        public void Initialize (ShipTransform shipTransform)
        {
            _spaceShipTransform = shipTransform.transform;
        }

        public async UniTask CreateUFO(Vector2 position)
        {
            var ufoGameObject = await _addressablesLoader.LoadUFOPrefab();
            var ufoInstance = _container.InstantiatePrefab(ufoGameObject, position, Quaternion.identity, null);
            UFO ufoComponent = ufoInstance.GetComponent<UFO>();
            ufoComponent.SetLoadedPrefab(ufoGameObject);
            ufoComponent.Initialize(_spaceShipTransform, _gameStateManager);
            _gameStateManager.RegisterListener(ufoComponent);
            OnUFOCreated?.Invoke(ufoComponent);
        }
    }
}
