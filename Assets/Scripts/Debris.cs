using UnityEngine;

public class Debris : MonoBehaviour
{
    private float speed = 3f;
    private Vector2 randomDirection;

    private void Start()
    {
        randomDirection = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        transform.Translate(randomDirection * speed * Time.deltaTime);
    }
}