using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] GameObject debrisPrefab;
    private float _speed = 3f;
    private Vector2 _randomDirection;

    private void Start()
    {
        _randomDirection = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        transform.Translate(_randomDirection * _speed * Time.deltaTime);
        TeleportIfOutOfBound();
    }

    public void Shatter()
    {
        for (int i = 0; i < 5; i++) 
        {
            GameObject debris =  Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            if(debris != null)
            Destroy(debris, 2f);
        }
        Destroy(gameObject);
    }

    void TeleportIfOutOfBound()
    {
        Camera camera = Camera.main;
        Vector3 viewPos = transform.position;
        Vector3 cameraBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        if (viewPos.x > cameraBounds.x) viewPos.x = -cameraBounds.x;
        if (viewPos.x < -cameraBounds.x) viewPos.x = cameraBounds.x;
        if (viewPos.y > cameraBounds.y) viewPos.y = -cameraBounds.y;
        if (viewPos.y < -cameraBounds.y) viewPos.y = cameraBounds.y;
        transform.position = viewPos;
    }
}
