using UnityEngine;

public class UFO : MonoBehaviour, IUfo
{
    private Transform _spaceShipTransform;
    private float _speed = 2f;

    private void Update()
    {
        ShipChase();
        TeleportIfOutOfBound();
    }

    public void Initialize(Transform spaceShipTransform)
    {
        _spaceShipTransform = spaceShipTransform;
    }

    void ShipChase()
    {
        if (_spaceShipTransform != null)
        {
            Vector2 direction = (_spaceShipTransform.position - transform.position).normalized;
            transform.Translate(direction * _speed * Time.deltaTime);
        }
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
