using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class UIShowing : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coordinatesText;
        [SerializeField] private TextMeshProUGUI angleText;
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private TextMeshProUGUI laserChargesText;
        [SerializeField] private TextMeshProUGUI laserCooldownText;
        [SerializeField] private TextMeshProUGUI scoreText;

        private SpaceShipShooting _spaceShip;
        private Score _score;
        private Vector2 _previousPosition;
        private float _speed;

        public void Initialize(SpaceShipShooting spaceShip, Score score)
        {
            _spaceShip = spaceShip;
            _score = score;
        }

        private void Update()
        {
            if (_spaceShip == null || _score == null) 
                return;

            Vector2 currentPosition = _spaceShip.transform.position;
            float distanceMoved = Vector2.Distance(_previousPosition, currentPosition);
            _speed = distanceMoved / Time.deltaTime;
            _previousPosition = currentPosition;

            coordinatesText.text = "Coordinates: " + _spaceShip.transform.position;
            angleText.text = "Angle: " + _spaceShip.transform.eulerAngles.z;
            speedText.text = "Speed: " + _speed;
            laserChargesText.text = "Laser Charges: " + _spaceShip.currentLaserShots;
            laserCooldownText.text = "Laser Cooldown: " + _spaceShip.laserCooldown;
            scoreText.text = "Score: " + _score.Count;
        }
    }
}
