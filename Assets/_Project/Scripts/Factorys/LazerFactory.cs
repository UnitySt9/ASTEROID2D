using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class LazerFactory
    {
        private const string LAZER_PREFAB_KEY = "lazer_prefab";
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
            var lazerPrefab = await _addressablesLoader.LoadPrefabAsync(LAZER_PREFAB_KEY);
            var lazer = _container.InstantiatePrefabForComponent<Lazer>(lazerPrefab, firePoint.position, firePoint.rotation, null);
            lazer.SetLoadedPrefab(lazerPrefab);
        }
    }
}
