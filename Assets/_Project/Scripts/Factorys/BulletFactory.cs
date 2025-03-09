using UnityEngine;

namespace _Project.Scripts
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        
        public void CreateBullet(Transform firePoint)
        {
            Bullet bullet = Instantiate(_bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetSpeed(firePoint);
        }
    }
}
