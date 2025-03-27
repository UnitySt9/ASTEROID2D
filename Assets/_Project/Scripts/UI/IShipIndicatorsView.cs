using UnityEngine;

namespace _Project.Scripts
{
    public interface IShipIndicatorsView
    {
        void UpdateCoordinates(Vector2 position);
        void UpdateAngle(float angle);
        void UpdateSpeed(float speed);
        void UpdateLaserCharges(int charges);
        void UpdateLaserCooldown(float cooldown);
        void UpdateScore(int score);
    }
}
