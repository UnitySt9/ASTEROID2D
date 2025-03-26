using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    public class UFOFactory
    {
        public event Action<UFO> OnUFOCreated;

        private UFO _ufoPrefab;
        private GameStateManager _gameStateManager;
        private Transform _spaceShipTransform;
        private ShipTransform _shipTransform;

        public UFOFactory(UFO ufoPrefab, GameStateManager gameStateManager, ShipTransform shipTransform)
        {
            _ufoPrefab = ufoPrefab;
            _gameStateManager = gameStateManager;
            _shipTransform = shipTransform;
        }

        public void Initialize()
        {
            _spaceShipTransform = _shipTransform.transform;
        }
        public void CreateUFO(Vector2 position)
        {
            UFO ufoInstance = Object.Instantiate(_ufoPrefab, position, Quaternion.identity);
            ufoInstance.Initialize(_spaceShipTransform, _gameStateManager);
            _gameStateManager.RegisterListener(ufoInstance);
            OnUFOCreated?.Invoke(ufoInstance);
        }
    }
}
