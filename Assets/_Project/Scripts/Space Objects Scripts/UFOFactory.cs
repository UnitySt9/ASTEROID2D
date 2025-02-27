using UnityEngine;

namespace _Project.Scripts
{
    public class UFOFactory
    {
        private readonly float _speed = 2f;
        private UFO _ufo;
        private Vector2 _direction = Random.insideUnitCircle.normalized;
        
        public UFOFactory(UFO ufo)
        {
            _ufo = ufo;
        }
        
        public void CreateUFO(Vector2 position, Transform spaceShipTransform)
        {
            UFO ufoInstance = Object.Instantiate(_ufo, position, Quaternion.identity);
            UFO ufoComponent = ufoInstance.GetComponent<UFO>();
            Rigidbody2D rigidbody2D = ufoComponent.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = _direction * _speed;
            ufoComponent.Initialize(spaceShipTransform);
        }
    }
}
