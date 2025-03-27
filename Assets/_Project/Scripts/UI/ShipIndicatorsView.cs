using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class ShipIndicatorsView : MonoBehaviour, IShipIndicatorsView
    {
        [SerializeField] private TextMeshProUGUI coordinatesText;
        [SerializeField] private TextMeshProUGUI angleText;
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private TextMeshProUGUI laserChargesText;
        [SerializeField] private TextMeshProUGUI laserCooldownText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void UpdateCoordinates(Vector2 position)
        {
            coordinatesText.text = "Coordinates: " + position;
        }

        public void UpdateAngle(float angle)
        {
            angleText.text = "Angle: " + angle;
        }

        public void UpdateSpeed(float speed)
        {
            speedText.text = "Speed: " + speed;
        }

        public void UpdateLaserCharges(int charges)
        {
            laserChargesText.text = "Laser Charges: " + charges;
        }

        public void UpdateLaserCooldown(float cooldown)
        {
            laserCooldownText.text = "Laser Cooldown: " + cooldown;
        }

        public void UpdateScore(int score)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
