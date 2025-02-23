using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    public class SpaceShipShooting : MonoBehaviour
    {
        public float laserCooldown = 5f;
        public int currentLaserShots;
        
        [SerializeField] GameObject _bulletPrefab;
        [SerializeField] Transform _firePoint;
        [SerializeField] GameObject _laserPrefab;
        [SerializeField] Score _score;
        
        private float _bulletSpeed = 10;
        private int _maxLaserShots = 3;

        private void Start()
        {
            currentLaserShots = _maxLaserShots;
            StartCoroutine(RechargeLaserCoroutine());
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && currentLaserShots > 0)
            {
                ShootLaser();
            }
        }
        
        private void Shoot()
        {
            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = _firePoint.up * _bulletSpeed;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            _score.SubscribeToBullet(bulletScript);
            Destroy(bullet, 2f);
            bulletScript.Initialize(_score);
        }

        private void ShootLaser()
        {
            GameObject lazer = Instantiate(_laserPrefab, _firePoint.position, _firePoint.rotation);
            Lazer lazerScript = lazer.GetComponent<Lazer>();
            _score.SubscribeToLazer(lazerScript);
            lazerScript.Initialize(_score);
            currentLaserShots--;
        }
        
        private IEnumerator RechargeLaserCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(laserCooldown);
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
