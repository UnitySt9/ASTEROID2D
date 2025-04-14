using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class ShipIndicatorsPresenter : ITickable
    {
        private IShipIndicatorsView _view;
        private SpaceShipShooting _spaceShip;
        private Score _score;
        private Vector2 _previousPosition;
        private float _speed;

        public void Initialize(IShipIndicatorsView view, SpaceShipShooting spaceShip, Score score)
        {
            _view = view;
            _spaceShip = spaceShip;
            _score = score;
        }
        
        public void Tick()
        {
            if (_spaceShip == null || _score == null)
                return;

            Vector2 currentPosition = _spaceShip.transform.position;
            float distanceMoved = Vector2.Distance(_previousPosition, currentPosition);
            _speed = distanceMoved / Time.deltaTime;
            _previousPosition = currentPosition;

            var message = new ShipIndicatorsMessage
            {
                Position = _spaceShip.transform.position,
                Angle = _spaceShip.transform.eulerAngles.z,
                Speed = _speed,
                LaserCharges = _spaceShip.currentLaserShots,
                LaserCooldown = _spaceShip.laserCooldown,
                Score = _score.Count
            };

            _view.UpdateIndicators(message);
        }
    }
}
