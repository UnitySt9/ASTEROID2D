using UnityEngine;
using TMPro;

public class SpaceShipController : MonoBehaviour
{
    // �������
    public float acceleration = 5f; // ���������
    public float maxSpeed = 10f; // ������������ ��������
    private float currentSpeed = 0f;
    public float rotationSpeed = 200f; // �������� ��������
    // ����
    public GameObject bulletPrefab; // ������ ����
    public Transform firePoint; // �����, �� ������� ����� �������� ����
    public float bulletSpeed = 10; // �������� ����
    // �����
    public GameObject laserPrefab; // ������ ������
    public static float laserCooldown = 5f; // ����� �����������
    public static int currentLaserShots;
    private int maxLaserShots = 3; // ������������ ���������� ���������

    public GameObject gameOverPanel;
    public TextMeshProUGUI endScore;
    void Start()
    {
        currentLaserShots = maxLaserShots;
        InvokeRepeating("RechargeLaser", laserCooldown, laserCooldown);
    }

    void Update()
    {
        // ���������� ���������
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -rotation);

        // �������� ������
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= acceleration * Time.deltaTime; // ������� ��� ���������� �������
        }
        
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); // ����������� ��������
        
        transform.position += transform.up * currentSpeed * Time.deltaTime; // �������� ������� ������
        
        TeleportIfOutOfBound();  // �������� ������ �� ������� ������

        // �������� ����
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        // �������� �����
        if (Input.GetKeyDown(KeyCode.Mouse1) && currentLaserShots > 0)
        {
            ShootLaser();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // ������� ����
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>(); // �������� Rigidbody2D ���� � ������ �� ��������
        bulletRb.velocity = firePoint.up * bulletSpeed;
        Destroy(bullet, 2f); // ���������� ���� ����� 2 �������
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
    // ����� �� �����
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
    // ������������ � �� ���������
    void OnCollisionEnter2D(Collision2D collision)
    {
        gameOverPanel.SetActive(true);
        endScore.text = "GAME OVER. SCORE: " + Score.score;
        Time.timeScale = 0;
    }
}
