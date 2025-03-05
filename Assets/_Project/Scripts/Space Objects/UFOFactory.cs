using UnityEngine;

namespace _Project.Scripts
{
    public class UFOFactory
    {
        private readonly float _speed = 2f;
        private UFO _ufo;
        private GameStateManager _gameStateManager;
        private Vector2 _direction = Random.insideUnitCircle.normalized;
        
        public UFOFactory(UFO ufo, GameStateManager gameStateManager)
        {
            _ufo = ufo;
            _gameStateManager = gameStateManager;
        }
        
        public void CreateUFO(Vector2 position, Transform spaceShipTransform, GameStateManager gameStateManager)
        {
            UFO ufoInstance = Object.Instantiate(_ufo, position, Quaternion.identity);
            UFO ufoComponent = ufoInstance.GetComponent<UFO>();
            Rigidbody2D rigidbody2D = ufoComponent.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = _direction * _speed;
            ufoComponent.Initialize(spaceShipTransform);
            ufoComponent.SetDependency(_gameStateManager);
        }
    }
}
