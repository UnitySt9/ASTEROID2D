using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class RestartGame : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        private ISceneLoader _sceneLoader;

        private void Start()
        {
            _sceneLoader = new SceneLoader();
            _restartButton.onClick.AddListener(ReloadScene);
            _menuButton.onClick.AddListener(LoadMenu);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(ReloadScene);
            _menuButton.onClick.RemoveListener(LoadMenu);
        }

        private void ReloadScene()
        {
            _sceneLoader.ReloadScene();
        }

        private void LoadMenu()
        {
            _sceneLoader.LoadMenu();
        }
    }
}
