using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : SpaceObject
    {
        [SerializeField] private Debris _debrisPrefab;
        private TeleportBounds _teleportBounds;
        private Camera _cameraMain;
        private GameObject _loadedPrefab;
        private IConfigService _configService;
        private IAudioService _audioService;
    
        [Inject]
        public void Construct(Camera cameraMain, IConfigService configService, IAudioService audioService)
        {
            _cameraMain = cameraMain;
            _configService = configService;
            _audioService = audioService;
            UpdateConfigValues();
            _configService.OnConfigUpdated += UpdateConfigValues;
        }

        protected override void Start()
        {
            base.Start();
            _teleportBounds = new TeleportBounds(transform, _cameraMain);
        }

        protected override void Update()
        {
            base.Update();
            _teleportBounds.BoundsUpdate();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            _audioService.PlayObjectExplosionSound();
            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Shatter();
            }
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_configService != null)
            {
                _configService.OnConfigUpdated -= UpdateConfigValues;
            }
        }
        
        private void UpdateConfigValues()
        {
            Speed = _configService.Config.spaceObjects.asteroidSpeed;
        }
        
        private void Shatter()
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(_debrisPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
