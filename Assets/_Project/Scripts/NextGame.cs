using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class NextGame : MonoBehaviour
    {
        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
