using UnityEngine;

namespace _Project.Scripts
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] GameObject debrisPrefab;
        private readonly float _speed = 3f;
        private Vector2 _randomDirection;
        private TeleportBounds _teleportBounds;

        private void Start()
        {
            _randomDirection = Random.insideUnitCircle.normalized;
            _teleportBounds = GetComponent<TeleportBounds>();
        }

        private void Update()
        {
            transform.Translate(_randomDirection * (_speed * Time.deltaTime));
            TeleportIfOutOfBound();
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Destroy(gameObject);
                Shatter();
            }
        }

        private void Shatter()
        {
            for (int i = 0; i < 5; i++) 
            {
                GameObject debris =  Instantiate(debrisPrefab, transform.position, Quaternion.identity);
                if(debris != null)
                    Destroy(debris, 2f);
            }
            Destroy(gameObject);
        }

        private void TeleportIfOutOfBound()
        {
            if (_teleportBounds != null)
            {
                transform.position = _teleportBounds.ConfineToBounds(transform.position);
            }
        }
    }
}
