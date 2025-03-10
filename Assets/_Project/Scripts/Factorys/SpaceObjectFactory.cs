using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class SpaceObjectFactory : MonoBehaviour
    {
        public event Action<SpaceObject> OnSpaceObjectCreated;
        
        [SerializeField] Asteroid _asteroid;
        [SerializeField] GameStateManager _gameStateManager;

        public void CreateAsteroid(Vector2 position)
        {
            Asteroid asteroid = Instantiate(_asteroid, position, Quaternion.identity);
            asteroid.Initialize(_gameStateManager);
            _gameStateManager.RegisterListener(asteroid);
            OnSpaceObjectCreated?.Invoke(asteroid);
        }
    }
}
