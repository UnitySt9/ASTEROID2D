namespace _Project.Scripts
{
    [System.Serializable]
    public class GameConfig
    {
        public ShipConfig ship;
        public WeaponsConfig weapons;
        public SpaceObjectsConfig spaceObjects;

        [System.Serializable]
        public class ShipConfig
        {
            public float maxSpeed;
            public float acceleration;
            public float rotationSpeed;
        }

        [System.Serializable]
        public class WeaponsConfig
        {
            public float laserCooldown;
            public int maxLaserShots;
        }

        [System.Serializable]
        public class SpaceObjectsConfig
        {
            public float ufoSpeed;
            public float asteroidSpeed;
        }
    }
}
