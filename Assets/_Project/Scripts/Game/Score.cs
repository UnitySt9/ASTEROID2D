namespace _Project.Scripts
{
    public class Score
    {
        public int Count { get; private set; }
        public int ObjectsDestroyed { get; private set; }

        private void AddScore(int amount)
        {
            Count += amount;
            ObjectsDestroyed++;
        }

        public void SubscribeToUfo(UFO ufo)
        {
            ufo.OnUFOHit += AddScore;
        }

        public void UnsubscribeFromUfo(UFO ufo)
        {
            ufo.OnUFOHit -= AddScore;
        }

        public void SubscribeToSpaceObject(SpaceObject spaceObject)
        {
            spaceObject.OnSpaceObjectHit += AddScore;
        }

        public void UnsubscribeFromSpaceObject(SpaceObject spaceObject)
        {
            spaceObject.OnSpaceObjectHit -= AddScore;
        }
    }
}