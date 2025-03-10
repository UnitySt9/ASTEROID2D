using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TeleportBounds))]
    public class Asteroid : SpaceObject
    {
        [SerializeField] private Debris _debrisPrefab;
        protected override void Start()
        {
            Speed = 3f;
            base.Start();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Shatter();
            }
        }
        
        private void Shatter()
        {
            for (int i = 0; i < 5; i++)
            {
                var debris = Instantiate(_debrisPrefab, transform.position, Quaternion.identity);
                debris.SetDependency(GameStateManager);
            }
            Destroy(gameObject);
        }
    }
}
