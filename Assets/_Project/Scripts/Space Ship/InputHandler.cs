using UnityEngine;

namespace _Project.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        private ShipMovement _shipMovement;
        private SpaceShipShooting _spaceShipShooting;
        private float _horizontal;
        private bool _isAccelerating;

        public void Initialize(ShipMovement shipMovement, SpaceShipShooting spaceShipShooting)
        {
            _shipMovement = shipMovement;
            _spaceShipShooting = spaceShipShooting;
        }

        private void Update()
        {
            HandleMovementInput();
            HandleShootingInput();
        }

        private void HandleMovementInput()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _isAccelerating = Input.GetKey(KeyCode.UpArrow);

            _shipMovement.HandleMovement(_horizontal, _isAccelerating);
        }

        private void HandleShootingInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _spaceShipShooting.Shoot();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _spaceShipShooting.ShootLaser();
            }
        }
    }
}
