using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class ShipIndicatorsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coordinatesText;
        [SerializeField] private TextMeshProUGUI angleText;
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private TextMeshProUGUI laserChargesText;
        [SerializeField] private TextMeshProUGUI laserCooldownText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void UpdateView(ShipIndicatorsModel model)
        {
            coordinatesText.text = "Coordinates: " + model.Position;
            angleText.text = "Angle: " + model.Angle;
            speedText.text = "Speed: " + model.Speed;
            laserChargesText.text = "Laser Charges: " + model.LaserCharges;
            laserCooldownText.text = "Laser Cooldown: " + model.LaserCooldown;
            scoreText.text = "Score: " + model.Score;
        }
    }
}
