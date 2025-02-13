using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _scoreValue = 1;
    private Score _score;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Asteroid>(out Asteroid asteroidComponent))
        {
            Asteroid asteroid = collision.GetComponent<Asteroid>();
            asteroid.Shatter();
            _score.AddScore(_scoreValue);
        }
        else if (collision.TryGetComponent<Debris>(out Debris debrisComponent) || collision.TryGetComponent<UFO>(out UFO ufoComponent))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            _score.AddScore(_scoreValue);
        }
    }

    public void Initialize(Score score)
    {
        _score = score;
    }
}
