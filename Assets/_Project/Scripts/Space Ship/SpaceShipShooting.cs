using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    public class SpaceShipShooting : MonoBehaviour
    {
        public float laserCooldown = 5f;
        public int currentLaserShots;
        
        [SerializeField] Transform _firePoint;
        [SerializeField] BulletFactory _bulletFactory;
        [SerializeField] LazerFactory _lazerFactory;

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
            _bulletFactory.CreateBullet(_firePoint);
        }
        
        public void ShootLaser()
        {
            if (currentLaserShots > 0)
            {
                _lazerFactory.CreateLazer(_firePoint);
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
