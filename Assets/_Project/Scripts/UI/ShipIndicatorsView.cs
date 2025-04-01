using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class ShipIndicatorsView : MonoBehaviour, IShipIndicatorsView
    {
        [SerializeField] private TextMeshProUGUI _coordinatesText;
        [SerializeField] private TextMeshProUGUI _angleText;
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private TextMeshProUGUI _laserChargesText;
        [SerializeField] private TextMeshProUGUI _laserCooldownText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void UpdateIndicators(ShipIndicatorsMessage message)
        {
            _coordinatesText.text = $"Coordinates: {message.Position}";
            _angleText.text = $"Angle: {message.Angle}";
            _speedText.text = $"Speed: {message.Speed}";
            _laserChargesText.text = $"Laser Charges: {message.LaserCharges}";
            _laserCooldownText.text = $"Laser Cooldown: {message.LaserCooldown}";
            _scoreText.text = $"Score: {message.Score}";
        }
    }
}
