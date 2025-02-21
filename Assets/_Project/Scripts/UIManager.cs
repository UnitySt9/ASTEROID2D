using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI coordinatesText;
        [SerializeField] TextMeshProUGUI angleText;
        [SerializeField] TextMeshProUGUI speedText;
        [SerializeField] TextMeshProUGUI laserChargesText;
        [SerializeField] TextMeshProUGUI laserCooldownText;
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] SpaceShipController spaceShip;
        [SerializeField] Score score;
        
        private Vector2 _previousPosition;
        private float _speed;

        private void FixedUpdate()
        {
            Vector2 currentPosition = spaceShip.transform.position;
            float distanceMoved = Vector2.Distance(_previousPosition, currentPosition);
            _speed = distanceMoved / Time.deltaTime;
            _previousPosition = currentPosition;
        }

        private void Update()
        {
            coordinatesText.text = "Coordinates: " + spaceShip.transform.position.ToString();
            angleText.text = "Angle: " + spaceShip.transform.eulerAngles.z.ToString();
            speedText.text = "Speed: " + _speed.ToString();
            laserChargesText.text = "Laser Charges: " + spaceShip.currentLaserShots.ToString();
            laserCooldownText.text = "Laser Cooldown: " + spaceShip.laserCooldown.ToString();
            scoreText.text = "Score: " + score.Count.ToString();
        }
    }
}
