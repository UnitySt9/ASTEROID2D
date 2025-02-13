using UnityEngine;

public class Score: MonoBehaviour
{
    public int score;

    public void AddScore(int amount)
    {
        score += amount;
    }
}
