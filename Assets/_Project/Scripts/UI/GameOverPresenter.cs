using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class GameOverPresenter : IDisposable, IGameStateListener
    {
        private readonly GameOverModel _model;
        private readonly GameStateManager _gameStateManager;
        private readonly Score _score;
        private readonly ISaveService _saveService;
        private readonly ICloudSaveService _cloudSaveService;
        private GameOverView _view;

        [Inject]
        public GameOverPresenter(
            GameOverModel model,
            GameStateManager gameStateManager,
            Score score,
            ISaveService saveService,
            ICloudSaveService cloudSaveService)
        {
            _model = model;
            _score = score;
            _gameStateManager = gameStateManager;
            _saveService = saveService;
            _cloudSaveService = cloudSaveService;
            _gameStateManager.RegisterListener(this);
        }

        public void Initialize(GameOverView view)
        {
            _view = view;
        }

        public async void OnGameOver()
        {
            var gameData = _saveService.Load();
            _model.Score = _score.Count;
            _model.IsGameOver = true;
            _model.IsNewRecord = _model.Score > gameData.HighScore;
            if (_model.IsNewRecord)
            {
                gameData.HighScore = _model.Score;
                gameData.SaveDateTime = DateTime.UtcNow;
                _saveService.Save(gameData);
                try
                {
                    await _cloudSaveService.SaveAsync(gameData);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to save to cloud: {e.Message}");
                }
            }
            _model.HighScore = gameData.HighScore;
            _view.ShowPanel(_model.Score, _model.HighScore, _model.IsNewRecord);
        }

        public void OnGameContinue()
        {
            _model.IsGameOver = false;
            _view.HidePanel();
        }

        public void Dispose()
        {
            _gameStateManager.UnregisterListener(this);
        }
    }
}

