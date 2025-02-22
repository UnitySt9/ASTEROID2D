using UnityEngine;

namespace _Project.Scripts
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] GameObject debrisPrefab;
        
        private readonly float _speed = 4f;
        private Vector2 _randomDirection;
        private TeleportBounds _teleportBounds;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _randomDirection = Random.insideUnitCircle.normalized;
            if (_teleportBounds == null)
                _teleportBounds = GetComponent<TeleportBounds>();
            _rb.velocity = _randomDirection * _speed;
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
                _rb.velocity = _randomDirection * _speed;
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
