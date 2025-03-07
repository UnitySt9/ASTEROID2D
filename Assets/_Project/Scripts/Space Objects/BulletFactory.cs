using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class BulletFactory : MonoBehaviour
    {
        public event Action<Bullet> OnBulletCreated;
        [SerializeField] private Bullet _bulletPrefab;
        
        public void CreateBullet(Transform firePoint)
        {
            Bullet bullet = Instantiate(_bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetSpeed(firePoint);
            OnBulletCreated?.Invoke(bullet);
        }
    }
}
