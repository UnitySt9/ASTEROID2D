using UnityEngine;

namespace _Project.Scripts
{
    public class BulletFactory
    {
        private Bullet _bulletPrefab;

        public BulletFactory(Bullet bulletPrefab)
        {
            _bulletPrefab = bulletPrefab;
        }

        public void CreateBullet(Transform firePoint)
        {
            Bullet bullet = Object.Instantiate(_bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetSpeed(firePoint);
        }
    }
}
