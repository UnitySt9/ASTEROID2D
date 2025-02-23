using UnityEngine;

namespace _Project.Scripts
{
    public class UFOFactory
    {
        private UFO _ufo;
        
        public UFOFactory(UFO ufo)
        {
            _ufo = ufo;
        }
        
        public void CreateUFO(Vector2 position, Transform spaceShipTransform)
        {
            UFO ufoInstance = Object.Instantiate(_ufo, position, Quaternion.identity);
            UFO ufoComponent = ufoInstance.GetComponent<UFO>();
            ufoComponent.Initialize(spaceShipTransform);
        }
    }
}
