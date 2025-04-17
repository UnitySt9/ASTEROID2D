using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class SpaceObject : MonoBehaviour, IGameStateListener
    {
        public event Action<int> OnSpaceObjectHit;
        public event Action<SpaceObject> OnSpaceObjectDestroyed;
        
        private readonly int _scoreValue = 1;
        protected float Speed;
        private GameStateManager _gameStateManager;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _direction;
        private bool _isGameOver = false;
        
        protected virtual void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _direction = Random.insideUnitCircle.normalized;
            _rigidbody2D.velocity = _direction * Speed;
        }

        protected virtual void Update()
        {
            if (_isGameOver)
            {
                _rigidbody2D.simulated = false;
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                OnSpaceObjectHit?.Invoke(_scoreValue);
                Destroy(gameObject);
            }
            else
            {
                _rigidbody2D.velocity = _direction * Speed;
            }
        }

        protected virtual void OnDestroy()
        {
            UnregisterFromGameStateManager();
            OnSpaceObjectDestroyed?.Invoke(this);
        }
        
        public void OnGameOver()
        {
            _isGameOver = true;
        }

        public void OnGameContinue()
        {
            _isGameOver = false;
            _rigidbody2D.simulated = true;
        }

        public void Initialize(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
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
