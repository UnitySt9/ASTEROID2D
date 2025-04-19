using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private readonly float _bulletSpeed = 10;
        private readonly float _timeOfDeath = 2f;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _timeOfDeath);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out ShipMovement _))
            {
                Destroy(gameObject);
            }
        }

        public void GetSpeed(Transform firePoint)
        {
            _rigidbody2D.velocity = firePoint.up * _bulletSpeed;
        }
    }
}
