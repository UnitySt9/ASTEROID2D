using UnityEngine;

namespace _Project.Scripts
{
    public class InputHandler : MonoBehaviour, IGameStateListener
    {
        [SerializeField] GameStateManager _gameStateManager;
        private ShipMovement _shipMovement;
        private SpaceShipShooting _spaceShipShooting;
        private bool _isGameOver = false;
        private float _horizontal;
        private bool _isAccelerating;

        private void Start()
        {
            _gameStateManager = FindObjectOfType<GameStateManager>();
            _gameStateManager.RegisterListener(this);
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
        }

        public void OnGameOver()
        {
            _isGameOver = true;
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
