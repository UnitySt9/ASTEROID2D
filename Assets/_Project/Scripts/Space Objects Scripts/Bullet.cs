using System;
using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    
    public class Bullet : MonoBehaviour
    {
        public event Action<int> OnBulletHit; 
        
        private readonly int _scoreValue = 1;
        private readonly float _bulletSpeed = 10;
        private Rigidbody2D _rigidbody2D;
        private Score _score;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Destroy(gameObject, 2f);
        }

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
        }
        
        public void Initialize(Score score)
        {
            _score = score;
        }
        
        public void GetSpeed( Transform firePoint)
        {
            _rigidbody2D.velocity = firePoint.up * _bulletSpeed;
        }
    }
}
