using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class SpaceShipController : MonoBehaviour
    {
        public float laserCooldown = 5f;
        public int currentLaserShots;
        
        [SerializeField] GameObject _bulletPrefab;
        [SerializeField] Transform _firePoint;
        [SerializeField] GameObject _laserPrefab;
        [SerializeField] GameObject _gameOverPanel;
        [SerializeField] TextMeshProUGUI _endScore;
        [SerializeField] Score _score;
        
        private TeleportBounds _teleportBounds;
        private float _acceleration = 5f;
        private float _maxSpeed = 10f; 
        private float _currentSpeed;
        private float _rotationSpeed = 200f;
        private float _bulletSpeed = 10;
        private int _maxLaserShots = 3;

        private void Start()
        {
            currentLaserShots = _maxLaserShots;
            _teleportBounds = GetComponent<TeleportBounds>();
            InvokeRepeating(nameof(RechargeLaser), laserCooldown, laserCooldown);
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
            TeleportIfOutOfBound();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && currentLaserShots > 0)
            {
                ShootLaser();
            }
        }

        private void OnCollisionEnter2D()
        {
            _gameOverPanel.SetActive(true);
            _endScore.text = "GAME OVER. SCORE: " + _score.Count;
            Time.timeScale = 0;
        }

        private void Shoot()
        {
            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = _firePoint.up * _bulletSpeed;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            _score.SubscribeToBullet(bulletScript);
            Destroy(bullet, 2f);
            bulletScript.Initialize(_score);
        }

        void ShootLaser()
        {
            GameObject lazer = Instantiate(_laserPrefab, _firePoint.position, _firePoint.rotation);
            Lazer lazerScript = lazer.GetComponent<Lazer>();
            _score.SubscribeToLazer(lazerScript);
            lazerScript.Initialize(_score);
            currentLaserShots--;
        }
        
        private void RechargeLaser()
        {
            if (currentLaserShots < _maxLaserShots)
            {
                currentLaserShots++;
            }
        }
        
        private void TeleportIfOutOfBound()
        {
            if (_teleportBounds != null)
            {
                transform.position = _teleportBounds.ConfineToBounds(transform.position);
            }
        }
    }
}
