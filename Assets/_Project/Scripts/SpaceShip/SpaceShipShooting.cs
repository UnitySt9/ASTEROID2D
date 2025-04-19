using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class SpaceShipShooting : MonoBehaviour
    {
        public int ShotsFired { get; private set; }
        public int LasersUsed { get; private set; }
        
        public float laserCooldown = 5f;
        public int currentLaserShots;
        
        [SerializeField] private Transform _firePoint;
        private BulletFactory _bulletFactory;
        private LazerFactory _lazerFactory;
        private IAnalyticsService _analyticsService;
        private WaitForSeconds _waitRechargeLaser;
        private int _maxLaserShots = 3;

        [Inject]
        private void Construct(BulletFactory bulletFactory, LazerFactory lazerFactory, IAnalyticsService analyticsService)
        {
            _bulletFactory = bulletFactory;
            _lazerFactory = lazerFactory;
            _analyticsService = analyticsService;
        }

        private void Start()
        {
            currentLaserShots = _maxLaserShots;
            _waitRechargeLaser = new WaitForSeconds(laserCooldown);
            StartCoroutine(RechargeLaserCoroutine());
        }

        public void Shoot()
        {
            _bulletFactory.CreateBullet(_firePoint);
            ShotsFired++;
        }
        
        public void ShootLaser()
        {
            if (currentLaserShots > 0)
            {
                _lazerFactory.CreateLazer(_firePoint);
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
