using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    private int scoreValue = 1;
    private Score _score;
    public void Initialize(Score score)
    {
        _score = score;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Asteroid>(out Asteroid asteroidComponent))
        {
            Asteroid asteroid = collision.GetComponent<Asteroid>();
            asteroid.Shatter();// Разрушение астероида
            _score.AddScore(scoreValue);
        }
        else if (collision.TryGetComponent<Debris>(out Debris debrisComponent) || collision.TryGetComponent<UFO>(out UFO ufoComponent))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            _score.AddScore(scoreValue);
        }
    }
}
