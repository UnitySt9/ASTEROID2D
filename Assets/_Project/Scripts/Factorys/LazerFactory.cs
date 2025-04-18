using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class LazerFactory
    {
        private DiContainer _container;
        private IAddressablesLoader _addressablesLoader;

        [Inject]
        public LazerFactory(DiContainer container, IAddressablesLoader addressablesLoader)
        {
            _container = container;
            _addressablesLoader = addressablesLoader;
        }

        public async UniTask CreateLazer(Transform firePoint)
        {
            var lazerPrefab = await _addressablesLoader.LoadLazerPrefab();
            var lazer = _container.InstantiatePrefabForComponent<Lazer>(lazerPrefab, firePoint.position, firePoint.rotation, null);
            lazer.SetLoadedPrefab(lazerPrefab);
        }
    }
}
