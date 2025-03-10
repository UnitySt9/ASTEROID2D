using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class UFOFactory : MonoBehaviour
    {
        public event Action<UFO> OnUFOCreated;
        
        [SerializeField] UFO _ufo;
        [SerializeField] GameStateManager _gameStateManager;
        private Transform _spaceShipTransform;
        
        public void CreateUFO(Vector2 position)
        {
            UFO ufoInstance = Instantiate(_ufo, position, Quaternion.identity);
            ufoInstance.Initialize(_spaceShipTransform, _gameStateManager);
            _gameStateManager.RegisterListener(ufoInstance);
            OnUFOCreated?.Invoke(ufoInstance);
        }

        public void Initialize(Transform spaceShipTransform)
        {
            _spaceShipTransform = spaceShipTransform;
        }
    }
}
