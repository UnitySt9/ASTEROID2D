using Zenject;

namespace _Project.Scripts
{
    public class InputHandler : IGameStateListener, ITickable
    {
        private GameStateManager _gameStateManager;
        private ShipMovement _shipMovement;
        private SpaceShipShooting _spaceShipShooting;
        private readonly IInputProvider _inputProvider;
        private bool _isGameOver = false;

        public InputHandler(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
            _inputProvider = new KeyboardInputProvider();
            _gameStateManager.RegisterListener(this);
        }

        public void Initialize(ShipMovement shipMovement, SpaceShipShooting spaceShipShooting)
        {
            _shipMovement = shipMovement;
            _spaceShipShooting = spaceShipShooting;
        }

        public void Tick()
        {
            if (!_isGameOver)
            {
                if (_shipMovement == null || _spaceShipShooting == null)
                    return;
    
                HandleMovementInput();
                HandleShootingInput();
            }
            else
            {
                _shipMovement.OffRigidBody();
            }
        }

        public void OnGameOver()
        {
            _isGameOver = true;
        }

        public void OnGameContinue()
        {
            _isGameOver = false;
            _shipMovement.OnRigidBody();
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
