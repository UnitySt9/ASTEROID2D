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

        public UFOFactory(UFO ufoPrefab, GameStateManager gameStateManager, Transform spaceShipTransform)
        {
            _ufoPrefab = ufoPrefab;
            _gameStateManager = gameStateManager;
            _spaceShipTransform = spaceShipTransform;
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
