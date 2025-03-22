using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class ShipIndicatorsPresenter : ITickable
    {
        private readonly ShipIndicatorsModel _model;
        private readonly ShipIndicatorsView _view;
        private readonly SpaceShipShooting _spaceShip;
        private readonly Score _score;

        private Vector2 _previousPosition;
        private float _speed;

        public ShipIndicatorsPresenter(ShipIndicatorsModel model, ShipIndicatorsView view, SpaceShipShooting spaceShip, Score score)
        {
            _model = model;
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

            _model.Position = _spaceShip.transform.position;
            _model.Angle = _spaceShip.transform.eulerAngles.z;
            _model.Speed = _speed;
            _model.LaserCharges = _spaceShip.currentLaserShots;
            _model.LaserCooldown = _spaceShip.laserCooldown;
            _model.Score = _score.Count;
            _view.UpdateView(_model);
        }
    }
}
