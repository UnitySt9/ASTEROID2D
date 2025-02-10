using UnityEngine;

public class Lazer : MonoBehaviour
{
    private float laserDuration = 0.5f;
    private int scoreValue = 1;
    private Score _score;
    public void Initialize(Score score)
    {
        _score = score;
    }
    private void Start()
    {
        Destroy(gameObject, laserDuration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Asteroid>(out Asteroid asteroidComponent) || collision.TryGetComponent<UFO>(out UFO ufoComponent) || collision.TryGetComponent<Debris>(out Debris debrisComponent))
        {
            Destroy(collision.gameObject);
            _score.AddScore(scoreValue);
        }
    }
}
