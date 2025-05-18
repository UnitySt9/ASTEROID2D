using System.Collections;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class SpaceShipShooting : MonoBehaviour
    {
        public int ShotsFired { get; private set; }
        public int LasersUsed { get; private set; }
        
        public float laserCooldown;
        public int currentLaserShots;
        
        [SerializeField] private Transform _firePoint;
        private BulletFactory _bulletFactory;
        private LazerFactory _lazerFactory;
        private IAnalyticsService _analyticsService;
        private IConfigService _configService;
        private WaitForSeconds _waitRechargeLaser;
        private IAudioService _audioService;
        private IVfxService _vfxService;
        private int _maxLaserShots;

        [Inject]
        private void Construct(
            BulletFactory bulletFactory, 
            LazerFactory lazerFactory, 
            IAnalyticsService analyticsService,
            IConfigService configService,
            IAudioService audioService,
            IVfxService vfxService)
        {
            _bulletFactory = bulletFactory;
            _lazerFactory = lazerFactory;
            _analyticsService = analyticsService;
            _configService = configService;
            _audioService = audioService;
            _vfxService = vfxService;
            UpdateConfigValues();
            _configService.OnConfigUpdated += UpdateConfigValues;
        }

        private void UpdateConfigValues()
        {
            laserCooldown = _configService.Config.weapons.laserCooldown;
            _maxLaserShots = _configService.Config.weapons.maxLaserShots;
            _waitRechargeLaser = new WaitForSeconds(laserCooldown);
            if (currentLaserShots > _maxLaserShots)
            {
                currentLaserShots = _maxLaserShots;
            }
        }

        private void Start()
        {
            currentLaserShots = _maxLaserShots;
            StartCoroutine(RechargeLaserCoroutine());
        }

        private void OnDestroy()
        {
            if (_configService != null)
            {
                _configService.OnConfigUpdated -= UpdateConfigValues;
            }
        }

        public void Shoot()
        {
            _bulletFactory.CreateBullet(_firePoint);
            _audioService.PlayShootSound();
            _vfxService.PlayShootVfx(_firePoint.position);
            ShotsFired++;
        }
        
        public void ShootLaser()
        {
            if (currentLaserShots > 0)
            {
                _lazerFactory.CreateLazer(_firePoint);
                _audioService.PlayShootSound();
                _vfxService.PlayShootVfx(_firePoint.position);
                currentLaserShots--;
                LasersUsed++;
                _analyticsService.TrackLaserUsed();
            }
        }

        private IEnumerator RechargeLaserCoroutine()
        {
            while (true)
            {
                yield return _waitRechargeLaser;
                RechargeLaser();
            }
        }

        private void RechargeLaser()
        {
            if (currentLaserShots < _maxLaserShots)
            {
                currentLaserShots++;
            }
        }
    }
}
