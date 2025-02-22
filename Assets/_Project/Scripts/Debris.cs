using UnityEngine;

namespace _Project.Scripts
{
    public class Debris : MonoBehaviour
    {
        private readonly float _speed = 6f;
        private Vector2 _randomDirection;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        private void Start()
        {
            _randomDirection = Random.insideUnitCircle.normalized;
        }

        private void Update()
        {
            SetSpeed();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Destroy(gameObject);
            }
        }
        
        private void SetSpeed()
         {
             _rb.velocity = _randomDirection * _speed;
         }
    }
}