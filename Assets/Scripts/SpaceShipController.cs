using UnityEngine;
using TMPro;

public class SpaceShipController : MonoBehaviour
{
    // Корабль
    public float acceleration = 5f; // ускорение
    public float maxSpeed = 10f; // максимальная скорость
    private float currentSpeed = 0f;
    public float rotationSpeed = 200f; // Скорость поворота
    // Пуля
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка, из которой будет вылетать пуля
    public float bulletSpeed = 10; // Скорость пули
    // Лазер
    public GameObject laserPrefab; // Префаб лазера
    public static float laserCooldown = 5f; // Время перезарядки
    public static int currentLaserShots;
    private int maxLaserShots = 3; // Максимальное количество выстрелов

    public GameObject gameOverPanel;
    public TextMeshProUGUI endScore;
    void Start()
    {
        currentLaserShots = maxLaserShots;
        InvokeRepeating("RechargeLaser", laserCooldown, laserCooldown);
    }

    void Update()
    {
        // Управление поворотом
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -rotation);

        // Движение вперед
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= acceleration * Time.deltaTime; // Инерция при отпускании клавиши
        }
        
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); // Ограничение скорости
        
        transform.position += transform.up * currentSpeed * Time.deltaTime; // Движение корабля вперед
        
        TeleportIfOutOfBound();  // Проверка выхода за границы экрана

        // Стрельба пули
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        // Стрельба лазер
        if (Input.GetKeyDown(KeyCode.Mouse1) && currentLaserShots > 0)
        {
            ShootLaser();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Создаем пулю
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D пули и задаем ей скорость
        bulletRb.velocity = firePoint.up * bulletSpeed;
        Destroy(bullet, 2f); // Уничтожить пулю через 2 секунды
    }

    private void ShootLaser()
    {
        Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
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
    // Столкновение с др объектами
    void OnCollisionEnter2D(Collision2D collision)
    {
        gameOverPanel.SetActive(true);
        endScore.text = "GAME OVER. SCORE: " + Score.score;
        Time.timeScale = 0;
    }
}
