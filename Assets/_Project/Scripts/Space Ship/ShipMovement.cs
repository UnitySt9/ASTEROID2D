using System;
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

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void HandleMovement(float horizontalInput, bool isAccelerating)
        {
            float rotation = horizontalInput * _rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, -rotation);

            if (isAccelerating)
            {
                _currentSpeed += _acceleration * Time.deltaTime;
            }
            else
            {
                _currentSpeed -= _acceleration * Time.deltaTime;
            }

            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
            transform.position += transform.up * (_currentSpeed * Time.deltaTime);
        }

        public void OffRigidBody()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.rotation = 0;
        }
    }
}
