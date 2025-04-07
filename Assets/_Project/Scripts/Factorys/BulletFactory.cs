using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class BulletFactory
    {
        private const string BULLET_PREFAB_KEY = "bullet_prefab";
        private DiContainer _container;
        private IAddressablesLoader _addressablesLoader;

        [Inject]
        public BulletFactory(DiContainer container, IAddressablesLoader addressablesLoader)
        {
            _container = container;
            _addressablesLoader = addressablesLoader;
        }

        public async UniTask CreateBullet(Transform firePoint)
        {
            var bulletPrefab = await _addressablesLoader.LoadPrefabAsync(BULLET_PREFAB_KEY);
            Bullet bullet = _container.InstantiatePrefabForComponent<Bullet>(bulletPrefab, firePoint.position, firePoint.rotation, null);
            bullet.SetLoadedPrefab(bulletPrefab);
            bullet.GetSpeed(firePoint);
        }
    }
}
