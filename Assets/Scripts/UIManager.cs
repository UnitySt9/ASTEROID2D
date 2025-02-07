using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coordinatesText;
    public TextMeshProUGUI angleText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI laserChargesText;
    public TextMeshProUGUI laserCooldownText;
    public TextMeshProUGUI scoreText;

    private Transform player;
    
    private Vector2 previousPosition;
    public float speed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = player.transform.position;
        float distanceMoved = Vector2.Distance(previousPosition, currentPosition);
        speed = distanceMoved / Time.deltaTime;
        previousPosition = currentPosition;
    }

    void Update()
    {
        // Обновление текстовых полей
        coordinatesText.text = "Coordinates: " + player.transform.position.ToString();
        angleText.text = "Angle: " + player.transform.eulerAngles.z.ToString();
        speedText.text = "Speed: " + speed.ToString();
        laserChargesText.text = "Laser Charges: " + SpaceShipController.currentLaserShots.ToString();
        laserCooldownText.text = "Laser Cooldown: " + SpaceShipController.laserCooldown.ToString();
        scoreText.text = "Score: " + Score.score.ToString();
    }
}
