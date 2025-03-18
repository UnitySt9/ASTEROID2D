namespace _Project.Scripts
{
    public class SpaceShipController
    {
        private ShipMovement _shipMovement;
        private SpaceShipShooting _spaceShipShooting;
        private InputHandler _inputHandler;
        
        public void Initialize(ShipMovement shipMovement, SpaceShipShooting spaceShipShooting, InputHandler inputHandler)
        {
            _shipMovement = shipMovement;
            _spaceShipShooting = spaceShipShooting;
            _inputHandler = inputHandler;
            _inputHandler.Initialize(_shipMovement, _spaceShipShooting);
        }
    }
}
