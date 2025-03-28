using System;
using Zenject;

namespace _Project.Scripts
{
    public class GameOverPresenter : IDisposable, IGameStateListener
    {
        private readonly GameOverModel _model;
        private readonly GameOverView _view;
        private readonly GameStateManager _gameStateManager;
        private readonly Score _score;
        private readonly ISaveService _saveService;

        [Inject]
        public GameOverPresenter(
            GameOverModel model,
            GameOverView view,
            GameStateManager gameStateManager,
            Score score,
            ISaveService saveService)
        {
            _model = model;
            _view = view;
            _score = score;
            _gameStateManager = gameStateManager;
            _saveService = saveService;
            _gameStateManager.RegisterListener(this);
        }

        public void OnGameOver()
        {
            var gameData = _saveService.Load();
            _model.Score = _score.Count;
            _model.IsGameOver = true;
            _model.IsNewRecord = _model.Score > gameData.HighScore;
            if (_model.IsNewRecord)
            {
                gameData.HighScore = _model.Score;
                _saveService.Save(gameData); 
            }
            _model.HighScore = gameData.HighScore;
            _view.ShowPanel(_model.Score, _model.HighScore, _model.IsNewRecord);
        }

        public void Dispose()
        {
            _gameStateManager.UnregisterListener(this);
        }
    }
}

