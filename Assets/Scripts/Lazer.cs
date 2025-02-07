using UnityEngine;

public class Lazer : MonoBehaviour
{
    private float laserDuration = 0.5f; // ƒлительность существовани€ лазера

    private void Start()
    {
        Destroy(gameObject, laserDuration); // ”ничтожаем лазер через заданное врем€
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("UFO") || collision.CompareTag("Debris"))
        {
            Destroy(collision.gameObject); // ”ничтожаем объект, с которым пересекаетс€ лазер
            Score.score++;
        }
    }
}
