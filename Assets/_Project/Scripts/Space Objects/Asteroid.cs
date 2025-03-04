using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TeleportBounds))]
    
    public class Asteroid : MonoBehaviour, IGameStateListener
    {
        private readonly float _speed = 3f;
        [SerializeField] GameObject debrisPrefab;
        private GameStateManager _gameStateManager;
        private Vector2 _direction;
        private Rigidbody2D _rigidbody2D;
        private bool _isGameOver = false;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _direction = Random.insideUnitCircle.normalized;
            _rigidbody2D.velocity = _direction * _speed;
            _gameStateManager = FindObjectOfType<GameStateManager>();
            _gameStateManager.RegisterListener(this);
        }
        
        private void Update()
        {
            if (_isGameOver)
            {
                _rigidbody2D.velocity = Vector2.zero * 0;
                _rigidbody2D.rotation = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Shatter();
            }

            if (collision.TryGetComponent(out ShipMovement _))
            {
                _rigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                _rigidbody2D.velocity = _direction * _speed;
            }
        }

        private void OnDestroy()
        {
            _gameStateManager.UnregisterListener(this);
        }
        
        public void OnGameOver()
        {
            _isGameOver = true;
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
