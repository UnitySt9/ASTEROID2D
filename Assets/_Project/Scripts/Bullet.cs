using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class Bullet : MonoBehaviour
    {
        public event Action<int> OnBulletHit; 
        
        private readonly int _scoreValue = 1;
        private Score _score;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<Rigidbody2D>() != null)
                OnBulletHit?.Invoke(_scoreValue);
        }
        
        private void OnDestroy()
        {
            if (_score != null)
            {
                _score.UnsubscribeFromBullet(this);
            }
            OnBulletHit = null;
        }
        
        public void Initialize(Score score)
        {
            _score = score;
        }
    }
}
