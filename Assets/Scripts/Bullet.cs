using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            // Логика разрушения астероида
            Asteroid asteroid = collision.GetComponent<Asteroid>();
            asteroid.Shatter();
            Score.score++;
        }
        else if (collision.CompareTag("Debris") || collision.CompareTag("UFO"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Score.score++;
        }
    }
}
