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
        private readonly float _timeOfDeath = 2f;
        private Rigidbody2D _rigidbody2D;
        private Score _score;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _timeOfDeath);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<ShipMovement>() == null)
            {
                OnBulletHit?.Invoke(_scoreValue);
            }
        }

        private void OnDestroy()
        {
            _score.UnsubscribeFromBullet(this);
        }

        public void Initialize(Score score)
        {
            _score = score;
        }

        public void GetSpeed(Transform firePoint)
        {
            _rigidbody2D.velocity = firePoint.up * _bulletSpeed;
        }
    }
}
