using UnityEngine;

namespace _Project.Scripts
{
    public class UFO : MonoBehaviour, IUfo
    {
        private Transform _spaceShipTransform;
        private TeleportBounds _teleportBounds;
        private Rigidbody2D _rb;
        private Vector2 _randomDirection;
        
        private readonly float _speed = 2f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
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
                _rb.velocity = _randomDirection * _speed;
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
                _rb.velocity = direction * _speed;
            }
        }
    }
}
