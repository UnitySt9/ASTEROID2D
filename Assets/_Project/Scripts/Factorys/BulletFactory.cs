using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class BulletFactory: IDisposable
    {
        private DiContainer _container;
        private IAddressablesLoader _addressablesLoader;
        private GameObject _bulletPrefab;
        private bool _isInitialized;

        [Inject]
        public BulletFactory(DiContainer container, IAddressablesLoader addressablesLoader)
        {
            _container = container;
            _addressablesLoader = addressablesLoader;
        }

        public async UniTask Initialize()
        {
            if (_isInitialized) return;

            _bulletPrefab = await _addressablesLoader.LoadBulletPrefab();
            _isInitialized = true;
        }

        public void CreateBullet(Transform firePoint)
        {
            var bullet = _container.InstantiatePrefabForComponent<Bullet>(_bulletPrefab, firePoint.position, firePoint.rotation, null);
            bullet.GetSpeed(firePoint);
        }


        public void Dispose()
        {
            _addressablesLoader.ReleaseAsset(_bulletPrefab);
            _bulletPrefab = null;
        }
    }
}
