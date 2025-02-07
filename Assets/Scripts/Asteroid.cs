using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Движение
    public float speed = 2f;
    private Vector2 randomDirection;

    // Осколки
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
    
    // Разрушение астероида
    public void Shatter()
    {
        // Создание обломков и увеличение их скорости
        for (int i = 0; i < 5; i++) // кол-во обломков
        {
            GameObject debris = Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();
            rb.velocity *= speedIncrement; // Увеличение скорости обломков
            if(debris != null)
            Destroy(debris, 2f);
        }

        Destroy(gameObject); // Уничтожаем астероид
    }
}
