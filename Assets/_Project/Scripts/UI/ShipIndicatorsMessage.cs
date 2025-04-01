using UnityEngine;

namespace _Project.Scripts
{
    public struct ShipIndicatorsMessage
    {
        public Vector2 Position;
        public float Angle;
        public float Speed;
        public int LaserCharges;
        public float LaserCooldown;
        public int Score;
    }
}
