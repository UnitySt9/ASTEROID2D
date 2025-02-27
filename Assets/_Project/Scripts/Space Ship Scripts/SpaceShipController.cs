using UnityEngine;

namespace _Project.Scripts
{
    public class SpaceShipController : MonoBehaviour
    {
        [SerializeField] ShipMovement _shipMovement;
        [SerializeField] SpaceShipShooting _spaceShipShooting;
        [SerializeField] InputHandler _inputHandler;

        private void Start()
        {
            _inputHandler.Initialize(_shipMovement, _spaceShipShooting);
        }
    }
}
