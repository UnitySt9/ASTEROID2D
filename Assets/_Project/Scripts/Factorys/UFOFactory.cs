using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class UFOFactory
    {
        public event Action<UFO> OnUFOCreated;

        private UFO _ufoPrefab;
        private GameStateManager _gameStateManager;
        private Transform _spaceShipTransform;
        private ShipTransform _shipTransform;
        private DiContainer _container;

        public UFOFactory(UFO ufoPrefab, GameStateManager gameStateManager, ShipTransform shipTransform, DiContainer container)
        {
            _ufoPrefab = ufoPrefab;
            _gameStateManager = gameStateManager;
            _shipTransform = shipTransform;
            _container = container;
        }

        public void Initialize()
        {
            _spaceShipTransform = _shipTransform.transform;
        }
        public void CreateUFO(Vector2 position)
        {
            UFO ufoInstance = _container.InstantiatePrefabForComponent<UFO>(_ufoPrefab, position, Quaternion.identity, null);
            ufoInstance.Initialize(_spaceShipTransform, _gameStateManager);
            _gameStateManager.RegisterListener(ufoInstance);
            OnUFOCreated?.Invoke(ufoInstance);
        }
    }
}
