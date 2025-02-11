using UnityEngine;
using TMPro;
public class SpaceShipController : MonoBehaviour
{
    // Корабль
    private float acceleration = 5f;
    private float maxSpeed = 10f; 
    private float currentSpeed = 0f;
    private float rotationSpeed = 200f;
    // Пуля
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint; 
    private float bulletSpeed = 10;
    // Лазер
    [SerializeField] GameObject laserPrefab; 
    public float laserCooldown = 5f; 
    public int currentLaserShots;
    private int maxLaserShots = 3;
    // Счёт
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI endScore;
    [SerializeField] Score score;
    void Start()
    {
        currentLaserShots = maxLaserShots;
        InvokeRepeating("RechargeLaser", laserCooldown, laserCooldown);
    }
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -rotation);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= acceleration * Time.deltaTime; 
        }
        
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); 
        transform.position += transform.up * currentSpeed * Time.deltaTime;
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
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = firePoint.up * bulletSpeed;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Initialize(score);
        Destroy(bullet, 2f);
    }
    private void ShootLaser()
    {
        GameObject lazer = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Lazer lazerScript = lazer.GetComponent<Lazer>();
        lazerScript.Initialize(score);
        currentLaserShots--;
    }
    private void RechargeLaser()
    {
        if (currentLaserShots < maxLaserShots)
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        gameOverPanel.SetActive(true);
        endScore.text = "GAME OVER. SCORE: " + score.score;
        Time.timeScale = 0;
    }
}
