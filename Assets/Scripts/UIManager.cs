using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coordinatesText;
    [SerializeField] TextMeshProUGUI angleText;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI laserChargesText;
    [SerializeField] TextMeshProUGUI laserCooldownText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] SpaceShipController spaceShip;
    [SerializeField] Score score;
    private Vector2 previousPosition;
    private float speed;
    private void FixedUpdate()
    {
        Vector2 currentPosition = spaceShip.transform.position;
        float distanceMoved = Vector2.Distance(previousPosition, currentPosition);
        speed = distanceMoved / Time.deltaTime;
        previousPosition = currentPosition;
    }
    void Update()
    {
        coordinatesText.text = "Coordinates: " + spaceShip.transform.position.ToString();
        angleText.text = "Angle: " + spaceShip.transform.eulerAngles.z.ToString();
        speedText.text = "Speed: " + speed.ToString();
        laserChargesText.text = "Laser Charges: " + spaceShip.currentLaserShots.ToString();
        laserCooldownText.text = "Laser Cooldown: " + spaceShip.laserCooldown.ToString();
        scoreText.text = "Score: " + score.score.ToString();
    }
}
