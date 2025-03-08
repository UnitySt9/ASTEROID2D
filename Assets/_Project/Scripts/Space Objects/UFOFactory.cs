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
            Rigidbody2D rigidbody2D = ufoInstance.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = _direction * _speed;
            ufoInstance.Initialize(spaceShipTransform);
            ufoInstance.SetDependency(_gameStateManager);
        }
    }
}
