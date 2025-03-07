using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class RestartGame : MonoBehaviour
    {
        [SerializeField] Button _restartButton;

        private void Start()
        {
            _restartButton.onClick.AddListener(RestartLevel);
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
