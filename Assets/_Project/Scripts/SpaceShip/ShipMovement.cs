using UnityEngine;

namespace _Project.Scripts
{
    public class ShipMovement : MonoBehaviour
    {
        private readonly float _maxSpeed = 10f;
        private Rigidbody2D _rigidbody2D;
        private float _acceleration = 5f;
        private float _currentSpeed;
        private float _rotationSpeed = 200f;
        private float _rotationInput;
        private bool _isAccelerating;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_rigidbody2D.simulated)
            {
                _rigidbody2D.angularVelocity = -_rotationInput * _rotationSpeed;
            
                if (_isAccelerating)
                {
                    _currentSpeed += _acceleration * Time.fixedDeltaTime;
                }
                else
                {
                    _currentSpeed -= _acceleration * Time.fixedDeltaTime;
                }

                _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
                _rigidbody2D.velocity = transform.up * _currentSpeed;
            }
        }

        public void HandleMovement(float horizontalInput, bool isAccelerating)
        {
            _rotationInput = horizontalInput;
            _isAccelerating = isAccelerating;
        }

        public void OffRigidBody()
        {
            _rigidbody2D.simulated = false;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
        }
    
        public void OnRigidBody()
        {
            if (_rigidbody2D != null)
            {
                _rigidbody2D.simulated = true;
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.angularVelocity = 0f;
                _currentSpeed = 0f;
                _rotationInput = 0f;
                _isAccelerating = false;
            }
        }
    }
}
