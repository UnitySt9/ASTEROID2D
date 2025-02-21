using UnityEngine;

namespace _Project.Scripts
{
    public class UFO : MonoBehaviour, IUfo
    {
        private Transform _spaceShipTransform;
        private TeleportBounds _teleportBounds;
        
        private readonly float _speed = 2f;

        private void Start()
        {
            _teleportBounds = GetComponent<TeleportBounds>();
        }

        private void Update()
        {
            FollowTheShip();
            TeleportIfOutOfBound();
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Destroy(gameObject);
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
                transform.Translate(direction * (_speed * Time.deltaTime));
            }
        }

        private void TeleportIfOutOfBound()
        {
            if (_teleportBounds != null)
            {
                transform.position = _teleportBounds.ConfineToBounds(transform.position);
            }
        }
    }
}
