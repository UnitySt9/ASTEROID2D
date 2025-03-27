using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class ShipIndicatorsPresenter : ITickable
    {
        private readonly IShipIndicatorsView _view;
        private readonly SpaceShipShooting _spaceShip;
        private readonly Score _score;
        private Vector2 _previousPosition;
        private float _speed;

        public ShipIndicatorsPresenter(IShipIndicatorsView view, SpaceShipShooting spaceShip, Score score)
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

            _view.UpdateCoordinates(_spaceShip.transform.position);
            _view.UpdateAngle(_spaceShip.transform.eulerAngles.z);
            _view.UpdateSpeed(_speed);
            _view.UpdateLaserCharges(_spaceShip.currentLaserShots);
            _view.UpdateLaserCooldown(_spaceShip.laserCooldown);
            _view.UpdateScore(_score.Count);
        }
    }
}
