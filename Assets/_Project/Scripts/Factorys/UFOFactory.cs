using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class UFOFactory
    {
        public event Action<UFO> OnUFOCreated;

        private const string UFO_PREFAB_KEY = "ufo_prefab";
        private GameStateManager _gameStateManager;
        private Transform _spaceShipTransform;
        private ShipTransform _shipTransform;
        private DiContainer _container;
        private IAddressablesLoader _addressablesLoader;

        public UFOFactory(
            GameStateManager gameStateManager, 
            ShipTransform shipTransform, 
            DiContainer container,
            IAddressablesLoader addressablesLoader)
        {
            _gameStateManager = gameStateManager;
            _shipTransform = shipTransform;
            _container = container;
            _addressablesLoader = addressablesLoader;
        }

        public void Initialize()
        {
            _spaceShipTransform = _shipTransform.transform;
        }

        public async UniTask CreateUFO(Vector2 position)
        {
            var ufoGameObject = await _addressablesLoader.LoadPrefabAsync(UFO_PREFAB_KEY);
            var ufoInstance = _container.InstantiatePrefab(ufoGameObject, position, Quaternion.identity, null);
            UFO ufoComponent = ufoInstance.GetComponent<UFO>();
            ufoComponent.SetLoadedPrefab(ufoGameObject);
            ufoComponent.Initialize(_spaceShipTransform, _gameStateManager);
            _gameStateManager.RegisterListener(ufoComponent);
            OnUFOCreated?.Invoke(ufoComponent);
        }
    }
}
