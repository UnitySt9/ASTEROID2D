using UnityEngine;

namespace _Project.Scripts
{
    public class ShipIndicatorsModel
    {
        public Vector2 Position { get; set; }
        public float Angle { get; set; }
        public float Speed { get; set; }
        public int LaserCharges { get; set; }
        public float LaserCooldown { get; set; }
        public int Score { get; set; }
    }
}
