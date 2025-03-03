using UnityEngine;

namespace _Project.Scripts
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] Bullet _bulletPrefab;
        [SerializeField] Score _score;

        public void CreateBullet(Transform firePoint)
        {
            Bullet bullet = Instantiate(_bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.Initialize(_score);
            bullet.GetSpeed(firePoint);
            _score.SubscribeToBullet(bullet);
        }
    }
}
