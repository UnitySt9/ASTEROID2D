using UnityEngine;

namespace _Project.Scripts
{
    public class SpaceShipController : MonoBehaviour
    {
        [SerializeField] ShipMovement _shipMovement;
        [SerializeField] SpaceShipShooting _spaceShipShooting;
        private GameStateManager _gameStateManager;
        private InputHandler _inputHandler;

        public void Initialize(InputHandler inputHandler, GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
            _inputHandler = inputHandler;
            _inputHandler.Initialize(_shipMovement, _spaceShipShooting);
        }
        
        private void OnCollisionEnter2D()
        {
            _gameStateManager?.GameOver();
        }
    }
}
