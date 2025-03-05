using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TeleportBounds))]
    public class Asteroid : SpaceObject, IGameStateListener
    {
        [SerializeField] private Debris debrisPrefab;
        private GameStateManager _gameStateManager;
        private bool _isGameOver = false;

        protected override void Start()
        {
            Speed = 3f;
            base.Start();
            _gameStateManager.RegisterListener(this);
        }

        protected override void Update()
        {
            base.Update();
            if (_isGameOver)
            {
                Rigidbody2D.velocity = Vector2.zero;
                Rigidbody2D.rotation = 0;
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Shatter(); 
            }

            if (collision.TryGetComponent(out ShipMovement _))
            {
                Rigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                Rigidbody2D.velocity = Direction * Speed;
            }
        }

        private void OnDestroy()
        {
            _gameStateManager.UnregisterListener(this);
        }

        public void SetDependency(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }
        
        public void OnGameOver()
        {
            _isGameOver = true;
        }

        private void Shatter()
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
