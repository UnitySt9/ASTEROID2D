using UnityEngine;

namespace _Project.Scripts
{
    public class Score: MonoBehaviour
    {
        public int Count {get; private set;}
        
        private void AddScore(int amount)
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
