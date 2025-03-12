using System;
using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TeleportBounds))]
    public class UFO : MonoBehaviour, IGameStateListener
    {
        public event Action<int> OnUFOHit;
        public event Action<UFO> OnUfoDestroyed;
        
        private readonly float _speed = 2f;
        private readonly int _scoreValue = 1;
        private Transform _spaceShipTransform;
        private GameStateManager _gameStateManager;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _direction;
        private bool _isGameOver = false;
        
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!_isGameOver)
            {
                FollowTheShip();
            }
            else
            {
                _rigidbody2D.simulated = false;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                OnUFOHit?.Invoke(_scoreValue);
                Destroy(gameObject);
            }
            else
            {
                _rigidbody2D.velocity = _direction * _speed;
            }
        }
        
        private void OnDestroy()
        {
            UnregisterFromGameStateManager();
            OnUfoDestroyed?.Invoke(this);
        }
        
        public void OnGameOver()
        {
            _isGameOver = true;
        }
        
        public void Initialize(Transform spaceShipTransform, GameStateManager gameStateManager)
        {
            _spaceShipTransform = spaceShipTransform;
            _gameStateManager = gameStateManager;
        }
        
        private void FollowTheShip()
        {
            if (_spaceShipTransform)
            {
                Vector2 direction = (_spaceShipTransform.position - transform.position).normalized;
                _rigidbody2D.velocity = direction * _speed;
            }
        }
        
        private void UnregisterFromGameStateManager()
        {
            if (_gameStateManager != null)
            {
                _gameStateManager.UnregisterListener(this);
            }
        }
    }
}
