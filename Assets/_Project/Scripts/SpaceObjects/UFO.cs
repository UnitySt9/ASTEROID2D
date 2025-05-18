using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UFO : MonoBehaviour, IGameStateListener
    {
        public event Action<int> OnUFOHit;
        public event Action<UFO> OnUfoDestroyed;
        
        private readonly int _scoreValue = 1;
        private Transform _spaceShipTransform;
        private GameStateManager _gameStateManager;
        private TeleportBounds _teleportBounds;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _direction;
        private Camera _cameraMain;
        private IConfigService _configService;
        private IAudioService _audioService;
        private IVfxService _vfxService;
        private bool _isGameOver = false;
        private float _speed;

        [Inject]
        public void Construct(
            Camera cameraMain, 
            IAddressablesLoader addressablesLoader, 
            IConfigService configService, 
            IAudioService audioService, 
            IVfxService vfxService)
        {
            _cameraMain = cameraMain;
            _configService = configService;
            _audioService = audioService;
            _vfxService = vfxService;
            UpdateConfigValues();
            _configService.OnConfigUpdated += UpdateConfigValues;
        }
        
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _teleportBounds = new TeleportBounds(transform, _cameraMain);
        }

        private void Update()
        {
            if (!_isGameOver)
            {
                FollowTheShip();
                _teleportBounds.BoundsUpdate();
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
                _audioService.PlayObjectExplosionSound();
                _vfxService.PlayObjectExplosionVfx(transform.position);
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
            
            if (_configService != null)
            {
                _configService.OnConfigUpdated -= UpdateConfigValues;
            }
        }
        
        public void Initialize(Transform spaceShipTransform, GameStateManager gameStateManager)
        {
            _spaceShipTransform = spaceShipTransform;
            _gameStateManager = gameStateManager;
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
        
        private void UpdateConfigValues()
        {
            _speed = _configService.Config.spaceObjects.ufoSpeed;
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
