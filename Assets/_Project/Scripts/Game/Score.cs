namespace _Project.Scripts
{
    public class Score
    {
        public int Count { get; private set; }

        public void AddScore(int amount)
        {
            Count += amount;
        }

        public void SubscribeToBullet(Bullet bullet)
        {
            bullet.OnBulletHit += AddScore;
        }

        public void UnsubscribeFromBullet(Bullet bullet)
        {
            bullet.OnBulletHit -= AddScore;
        }

        public void SubscribeToLazer(Lazer lazer)
        {
            lazer.OnLazerHit += AddScore;
        }

        public void UnsubscribeFromLazer(Lazer lazer)
        {
            lazer.OnLazerHit -= AddScore;
        }
    }
}
