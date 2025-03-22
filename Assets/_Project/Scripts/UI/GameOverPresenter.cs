using System;

namespace _Project.Scripts
{
    public class GameOverPresenter : IDisposable, IGameStateListener
    {
        private readonly GameOverModel _model;
        private readonly GameOverView _view;
        private readonly GameStateManager _gameStateManager;
        private readonly Score _score;

        public GameOverPresenter(GameOverModel model, GameOverView view, GameStateManager gameStateManager, Score score)
        {
            _model = model;
            _view = view;
            _score = score;
            _gameStateManager = gameStateManager;
            _gameStateManager.RegisterListener(this);
        }

        public void OnGameOver()
        {
            _model.Score = _score.Count;
            _model.IsGameOver = true;
            _view.ShowGameOverPanel(_model.Score);
        }

        public void Dispose()
        {
            _gameStateManager.UnregisterListener(this);
        }
    }
}

