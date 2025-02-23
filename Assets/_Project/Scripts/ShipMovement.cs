using UnityEngine;

namespace _Project.Scripts
{
    public class ShipMovement : MonoBehaviour
    {
        private TeleportBounds _teleportBounds;
        private float _acceleration = 5f;
        private float _maxSpeed = 10f; 
        private float _currentSpeed;
        private float _rotationSpeed = 200f;

        private void Start()
        {
            _teleportBounds = GetComponent<TeleportBounds>();
        }

        private void Update()
        {
            float rotation = Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, -rotation);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                _currentSpeed += _acceleration * Time.deltaTime;
            }
            else
            {
                _currentSpeed -= _acceleration * Time.deltaTime; 
            }
        
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed); 
            transform.position += transform.up * (_currentSpeed * Time.deltaTime);
            transform.position = _teleportBounds.ConfineToBounds(transform.position);
        }
    }
}
