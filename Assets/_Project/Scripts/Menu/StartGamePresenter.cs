using System;

namespace _Project.Scripts
{
    public class StartGamePresenter : IDisposable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly StartGameView _view;

        public StartGamePresenter(ISceneLoader sceneLoader, StartGameView view)
        {
            _sceneLoader = sceneLoader;
            _view = view;
            _view.StartButton.onClick.AddListener(StartLevel);
        }

        private void StartLevel()
        {
            _sceneLoader.LoadNextScene();
        }

        public void Dispose()
        {
            _view.StartButton.onClick.RemoveListener(StartLevel);
        }
    }
}
