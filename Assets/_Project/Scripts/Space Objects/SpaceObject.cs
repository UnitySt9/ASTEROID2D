using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class SpaceObject : MonoBehaviour
    {
        protected float Speed;
        protected Rigidbody2D Rigidbody2D;
        protected Vector2 Direction;

        protected virtual void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Direction = Random.insideUnitCircle.normalized;
            Rigidbody2D.velocity = Direction * Speed;
        }

        protected virtual void Update()
        {
            
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet _) || collision.TryGetComponent(out Lazer _))
            {
                Destroy(gameObject);
            }
        }

        protected void SetSpeed()
        {
            Rigidbody2D.velocity = Direction * Speed;
        }
    }
}
