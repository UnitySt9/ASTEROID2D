using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class RestartGame : MonoBehaviour
    {
        [SerializeField] Button _restartButton;
        [SerializeField] Button _menuButton;

        private void Start()
        {
            _restartButton.onClick.AddListener(RestartLevel);
            _menuButton.onClick.AddListener(LoadMenu);
        }
        
        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RestartLevel);
            _menuButton.onClick.RemoveListener(LoadMenu);
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void LoadMenu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
