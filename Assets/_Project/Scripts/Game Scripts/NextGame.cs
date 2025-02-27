using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class NextGame : MonoBehaviour
    {
        [SerializeField] Button _restartButton;

        private void Start()
        {
            _restartButton.onClick.AddListener(RestartGame);
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
