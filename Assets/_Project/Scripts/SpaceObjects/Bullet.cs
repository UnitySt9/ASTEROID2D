using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private readonly float _bulletSpeed = 10;
        private readonly float _timeOfDeath = 2f;
        private Rigidbody2D _rigidbody2D;
        private IAddressablesLoader _addressablesLoader;
        private GameObject _loadedPrefab;

        [Inject]
        public void Construct(IAddressablesLoader addressablesLoader)
        {
            _addressablesLoader = addressablesLoader;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _timeOfDeath);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out ShipMovement _))
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            _addressablesLoader.ReleaseAsset(_loadedPrefab);
        }

        public void SetLoadedPrefab(GameObject prefab)
        {
            _loadedPrefab = prefab;
        }

        public void GetSpeed(Transform firePoint)
        {
            _rigidbody2D.velocity = firePoint.up * _bulletSpeed;
        }
    }
}
