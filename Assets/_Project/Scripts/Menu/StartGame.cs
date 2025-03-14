using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] Button _startButton;

        private void Start()
        {
            _startButton.onClick.AddListener(StartLevel);
        }
        
        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartLevel);
        }

        private void StartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
