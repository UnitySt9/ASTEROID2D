using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class Lazer : MonoBehaviour
    {
        private readonly float _laserDuration = 0.5f;
        private IAddressablesLoader _addressablesLoader;
        private GameObject _loadedPrefab;

        [Inject]
        public void Construct(IAddressablesLoader addressablesLoader)
        {
            _addressablesLoader = addressablesLoader;
        }

        private void Start()
        {
            Destroy(gameObject, _laserDuration);
        }

        private void OnDestroy()
        {
            _addressablesLoader.ReleaseAsset(_loadedPrefab);
        }

        public void SetLoadedPrefab(GameObject prefab)
        {
            _loadedPrefab = prefab;
        }
    }
}
