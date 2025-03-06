using UnityEngine;

namespace _Project.Scripts
{
    public class SpaceShipController : MonoBehaviour
    {
        [SerializeField] private ShipMovement _shipMovement;
        [SerializeField] private SpaceShipShooting _spaceShipShooting;
        private InputHandler _inputHandler;

        public void Initialize(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _inputHandler.Initialize(_shipMovement, _spaceShipShooting);
        }
    }
}
