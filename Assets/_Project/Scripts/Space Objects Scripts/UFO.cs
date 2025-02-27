using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TeleportBounds))]
    
    public class UFO : MonoBehaviour
    {
        private readonly float _speed = 2f;
        private Transform _spaceShipTransform;
        private TeleportBounds _teleportBounds;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _randomDirection;
        
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _teleportBounds = GetComponent<TeleportBounds>();
            _randomDirection = Random.insideUnitCircle.normalized;
        }

        private void Update()
        {
            FollowTheShip();
            transform.position = _teleportBounds.ConfineToBounds(transform.position);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Destroy(gameObject);
            }
            else
            {
                _rigidbody2D.velocity = _randomDirection * _speed;
            }
        }
        public void Initialize(Transform spaceShipTransform)
        {
            _spaceShipTransform = spaceShipTransform;
        }
        
        private void FollowTheShip()
        {
            if (_spaceShipTransform)
            {
                Vector2 direction = (_spaceShipTransform.position - transform.position).normalized;
                _rigidbody2D.velocity = direction * _speed;
            }
        }
    }
}
