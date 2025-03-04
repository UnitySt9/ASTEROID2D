using UnityEngine;

namespace _Project.Scripts
{
    public class InputHandler : MonoBehaviour, IGameStateListener
    {
        [SerializeField] GameStateManager _gameStateManager;
        private ShipMovement _shipMovement;
        private SpaceShipShooting _spaceShipShooting;
        private IInputProvider _inputProvider;
        private bool _isGameOver = false;
        private float _horizontal;
        private bool _isAccelerating;

        private void Start()
        {
            _gameStateManager.RegisterListener(this);
            _inputProvider = new KeyboardInputProvider();
        }

        public void Initialize(ShipMovement shipMovement, SpaceShipShooting spaceShipShooting)
        {
            _shipMovement = shipMovement;
            _spaceShipShooting = spaceShipShooting;
        }

        private void Update()
        {
            if (!_isGameOver)
            {
                HandleMovementInput();
                HandleShootingInput();
            }
            else _shipMovement.OffRigidBody();
        }

        private void OnDestroy()
        {
            _gameStateManager.UnregisterListener(this);
        }
        
        public void OnGameOver()
        {
            _isGameOver = true;
        }
        
        private void HandleMovementInput()
        {
            float horizontal = _inputProvider.GetHorizontalAxis();
            bool isAccelerating = _inputProvider.IsAccelerating();

            _shipMovement.HandleMovement(horizontal, isAccelerating);
        }

        private void HandleShootingInput()
        {
            if (_inputProvider.IsShooting())
            {
                _spaceShipShooting.Shoot();
            }

            if (_inputProvider.IsShootingLaser())
            {
                _spaceShipShooting.ShootLaser();
            }
        }
    }
}
