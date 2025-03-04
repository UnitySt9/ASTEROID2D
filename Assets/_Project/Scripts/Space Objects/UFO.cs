using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TeleportBounds))]
    
    public class UFO : MonoBehaviour, IGameStateListener
    {
        private readonly float _speed = 2f;
        private Transform _spaceShipTransform;
        private Rigidbody2D _rigidbody2D;
        private GameStateManager _gameStateManager;
        private bool _isGameOver = false;
        
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _gameStateManager = FindObjectOfType<GameStateManager>();
            _gameStateManager.RegisterListener(this);
        }

        private void Update()
        {
            if (!_isGameOver)
            {
                FollowTheShip();
            }
            else
            {
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.rotation = 0;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Destroy(gameObject);
            }
            if (collision.TryGetComponent(out ShipMovement _))
            {
                _rigidbody2D.velocity = Vector2.zero;
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
