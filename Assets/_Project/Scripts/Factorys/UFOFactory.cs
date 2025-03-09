using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class UFOFactory : MonoBehaviour
    {
        public event Action<UFO> OnUFOCreated;
        
        [SerializeField]private UFO _ufo;
        private GameStateManager _gameStateManager;
        
        public void CreateUFO(Vector2 position, Transform spaceShipTransform, GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
            UFO ufoInstance = Instantiate(_ufo, position, Quaternion.identity);
            ufoInstance.Initialize(spaceShipTransform);
            ufoInstance.SetDependency(_gameStateManager);
            OnUFOCreated?.Invoke(ufoInstance);
        }
    }
}
