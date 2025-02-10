using UnityEngine;
public class Debris : MonoBehaviour
{
    private float speed = 5f;
    private Vector2 randomDirection;
    private void Start()
    {
        randomDirection = Random.insideUnitCircle.normalized;
    }
    private void Update()
    {
        SetSpeed();
    }
    void SetSpeed()
    {
        transform.Translate(randomDirection * speed * Time.deltaTime);
    }
}