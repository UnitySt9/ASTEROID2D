using UnityEngine;

namespace _Project.Scripts
{
    public class SpaceShipController : MonoBehaviour
    {
        [SerializeField] private ShipMovement _shipMovement;
        [SerializeField] private SpaceShipShooting _spaceShipShooting;
        [SerializeField] private InputHandler _inputHandler;

        private void Start()
        {
            _inputHandler.Initialize(_shipMovement, _spaceShipShooting);
        }
    }
}
