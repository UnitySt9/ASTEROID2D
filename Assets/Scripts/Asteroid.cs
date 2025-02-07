using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // ��������
    public float speed = 2f;
    private Vector2 randomDirection;

    // �������
    public GameObject debrisPrefab;
    private float speedIncrement = 1.5f;

    private void Start()
    {
        randomDirection = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        transform.Translate(randomDirection * speed * Time.deltaTime);
    }
    
    // ���������� ���������
    public void Shatter()
    {
        // �������� �������� � ���������� �� ��������
        for (int i = 0; i < 5; i++) // ���-�� ��������
        {
            GameObject debris = Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();
            rb.velocity *= speedIncrement; // ���������� �������� ��������
            if(debris != null)
            Destroy(debris, 2f);
        }

        Destroy(gameObject); // ���������� ��������
    }
}
