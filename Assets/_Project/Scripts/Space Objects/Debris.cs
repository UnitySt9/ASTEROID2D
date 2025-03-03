using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    
    public class Debris : MonoBehaviour
    {
        private readonly float _speed = 6f;
        private Vector2 _randomDirection;
        private Rigidbody2D _rigidbody2D;
        
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
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
             _rigidbody2D.velocity = _randomDirection * _speed;
         }
    }
}