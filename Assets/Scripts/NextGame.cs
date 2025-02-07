using UnityEngine;
using UnityEngine.SceneManagement;

public class NextGame : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
