using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts
{
    public class StartGamePresenter : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _startButton.onClick.AddListener(StartLevel);
        }
        
        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartLevel);
        }

        private void StartLevel()
        {
            _sceneLoader.LoadNextScene();
        }
    }
}
