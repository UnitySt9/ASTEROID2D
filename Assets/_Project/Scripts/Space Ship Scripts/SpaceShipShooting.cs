using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    public class SpaceShipShooting : MonoBehaviour
    {
        public float laserCooldown = 5f;
        public int currentLaserShots;

        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Lazer _laserPrefab;
        [SerializeField] private Score _score;

        private WaitForSeconds _waitRechargeLaser;
        private int _maxLaserShots = 3;

        private void Start()
        {
            currentLaserShots = _maxLaserShots;
            _waitRechargeLaser = new WaitForSeconds(laserCooldown);
            StartCoroutine(RechargeLaserCoroutine());
        }

        public void Shoot()
        {
            Bullet bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            bullet.GetSpeed(_firePoint);
            bullet.Initialize(_score);
            _score.SubscribeToBullet(bullet);
        }

        public void ShootLaser()
        {
            if (currentLaserShots > 0)
            {
                Lazer lazer = Instantiate(_laserPrefab, _firePoint.position, _firePoint.rotation);
                lazer.Initialize(_score);
                _score.SubscribeToLazer(lazer);
                currentLaserShots--;
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
