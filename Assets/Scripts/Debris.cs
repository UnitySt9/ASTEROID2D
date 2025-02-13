using UnityEngine;

public class Debris : MonoBehaviour
{
    private float _speed = 5f;
    private Vector2 _randomDirection;
    private void Start()
    {
        _randomDirection = Random.insideUnitCircle.normalized;
    }
    private void Update()
    {
        SetSpeed();
    }
    void SetSpeed()
    {
        transform.Translate(_randomDirection * _speed * Time.deltaTime);
    }
}