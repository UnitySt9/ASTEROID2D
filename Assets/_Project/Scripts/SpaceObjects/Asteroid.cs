using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : SpaceObject
    {
        [SerializeField] private Debris _debrisPrefab;
        private TeleportBounds _teleportBounds;
        private Camera _cameraMain;
        private GameObject _loadedPrefab;
    
        [Inject]
        public void Construct(Camera cameraMain)
        {
            _cameraMain = cameraMain;
        }
        
        protected override void Start()
        {
            Speed = 3f;
            base.Start();
            _teleportBounds = new TeleportBounds(transform, _cameraMain);
        }

        protected override void Update()
        {
            base.Update();
            _teleportBounds.BoundsUpdate();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Shatter();
            }
        }

        // protected override void OnDestroy()
        // {
        //     base.OnDestroy();
        //     _addressablesLoader.ReleaseAsset(_loadedPrefab);
        // }
        
        private void Shatter()
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(_debrisPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
