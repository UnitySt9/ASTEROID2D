using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class ShipMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private IConfigService _configService;
        private float _maxSpeed;
        private float _acceleration;
        private float _currentSpeed;
        private float _rotationSpeed;
        private float _rotationInput;
        private bool _isAccelerating;

        [Inject]
        public void Construct(IConfigService configService)
        {
            _configService = configService;
            UpdateConfigValues();
            _configService.OnConfigUpdated += UpdateConfigValues;
        }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnDestroy()
        {
            if (_configService != null)
            {
                _configService.OnConfigUpdated -= UpdateConfigValues;
            }
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
        
        private void UpdateConfigValues()
        {
            _maxSpeed = _configService.Config.ship.maxSpeed;
            _acceleration = _configService.Config.ship.acceleration;
            _rotationSpeed = _configService.Config.ship.rotationSpeed;
        }
    }
}
