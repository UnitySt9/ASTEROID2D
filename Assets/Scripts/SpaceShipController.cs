using UnityEngine;
using TMPro;
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
    private float _acceleration = 5f;
    private float _maxSpeed = 10f; 
    private float _currentSpeed = 0f;
    private float _rotationSpeed = 200f;
    private float _bulletSpeed = 10;
    private int _maxLaserShots = 3;

    void Start()
    {
        currentLaserShots = _maxLaserShots;
        InvokeRepeating("RechargeLaser", laserCooldown, laserCooldown);
    }
    void Update()
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
        transform.position += transform.up * _currentSpeed * Time.deltaTime;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        _gameOverPanel.SetActive(true);
        _endScore.text = "GAME OVER. SCORE: " + _score.score;
        Time.timeScale = 0;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = _firePoint.up * _bulletSpeed;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Initialize(_score);
        Destroy(bullet, 2f);
    }

    void ShootLaser()
    {
        GameObject lazer = Instantiate(_laserPrefab, _firePoint.position, _firePoint.rotation);
        Lazer lazerScript = lazer.GetComponent<Lazer>();
        lazerScript.Initialize(_score);
        currentLaserShots--;
    }
    void RechargeLaser()
    {
        if (currentLaserShots < _maxLaserShots)
        {
            currentLaserShots++;
        }
    }
    // Выход за экран
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
