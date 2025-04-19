using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class LazerFactory: IDisposable
    {
        private DiContainer _container;
        private IAddressablesLoader _addressablesLoader;
        private GameObject _lazerPrefab;
        private bool _isInitialized;

        [Inject]
        public LazerFactory(DiContainer container, IAddressablesLoader addressablesLoader)
        {
            _container = container;
            _addressablesLoader = addressablesLoader;
        }
        
        public async UniTask Initialize()
        {
            if (_isInitialized) return;

            _lazerPrefab = await _addressablesLoader.LoadLazerPrefab();
            _isInitialized = true;
        }

        public void CreateLazer(Transform firePoint)
        {
            _container.InstantiatePrefabForComponent<Lazer>(_lazerPrefab, firePoint.position, firePoint.rotation, null);
        }
        
        public void Dispose()
        {
            _addressablesLoader.ReleaseAsset(_lazerPrefab);
            _lazerPrefab = null;
        }
    }
}
