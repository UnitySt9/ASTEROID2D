using UnityEngine;

namespace _Project.Scripts
{
    public class Debris : MonoBehaviour
    {
        private readonly float _speed = 5f;
        private Vector2 _randomDirection;
        
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
            transform.Translate(_randomDirection * (_speed * Time.deltaTime));
        }
    }
}