using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TeleportBounds))]
    
    public class Asteroid : MonoBehaviour
    {
        private readonly float _speed = 3f;
        
        [SerializeField] GameObject debrisPrefab;
        private Vector2 _randomDirection;
        private TeleportBounds _teleportBounds;
        private Rigidbody2D _rigidbody2D;
        
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _randomDirection = Random.insideUnitCircle.normalized;
            _teleportBounds = GetComponent<TeleportBounds>();
            _rigidbody2D.velocity = _randomDirection * _speed;
        }

        private void FixedUpdate()
        {
            transform.position = _teleportBounds.ConfineToBounds(transform.position);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Shatter();
            }
            else
            {
                _rigidbody2D.velocity = _randomDirection * _speed;
            }
        }

        private void Shatter()
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject debris = Instantiate(debrisPrefab, transform.position, Quaternion.identity);
                if (debris != null)
                    Destroy(debris, 2f);
            }
            Destroy(gameObject);
        }
    }
}
