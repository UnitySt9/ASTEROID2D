using UnityEngine;

public class Lazer : MonoBehaviour
{
    private float laserDuration = 0.5f; // ������������ ������������� ������

    private void Start()
    {
        Destroy(gameObject, laserDuration); // ���������� ����� ����� �������� �����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("UFO") || collision.CompareTag("Debris"))
        {
            Destroy(collision.gameObject); // ���������� ������, � ������� ������������ �����
            Score.score++;
        }
    }
}
