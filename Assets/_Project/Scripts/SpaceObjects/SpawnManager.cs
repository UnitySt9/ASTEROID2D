using System;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts
{
    public class SpawnManager : IGameStateListener, IDisposable
    {
        private readonly int _spawnAsteroidInterval = 5;
        private readonly int _spawnUFOInterval = 4;

        private CancellationTokenSource _cancellationTokenSource;
        private SpaceObjectFactory _spaceObjectFactory;
        private UFOFactory _ufoFactory;
        private GameStateManager _gameStateManager;
        private Camera _camera;
        private Vector3 _cameraBounds;
        private bool _isGameOver = false;

        public SpawnManager(SpaceObjectFactory spaceObjectFactory, UFOFactory ufoFactory, GameStateManager gameStateManager)
        {
            _spaceObjectFactory = spaceObjectFactory;
            _ufoFactory = ufoFactory;
            _gameStateManager = gameStateManager;
        }

        public void Initialize()
        {
            _camera = Camera.main;
            _cancellationTokenSource = new CancellationTokenSource();
            StartSpawning();
            _gameStateManager.RegisterListener(this);
        }

        private void StartSpawning()
        {
            SpawnAsteroidsAsync(_cancellationTokenSource.Token).Forget();
            SpawnUFOsAsync(_cancellationTokenSource.Token).Forget();
        }

        public void OnGameOver()
        {
            _isGameOver = true;
            _cancellationTokenSource?.Cancel();
        }

        private async UniTaskVoid SpawnAsteroidsAsync(CancellationToken cancellationToken)
        {
            while (!_isGameOver && !cancellationToken.IsCancellationRequested)
            {
                SpawnAsteroid();
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnAsteroidInterval), cancellationToken: cancellationToken);
            }
        }

        private async UniTaskVoid SpawnUFOsAsync(CancellationToken cancellationToken)
        {
            while (!_isGameOver && !cancellationToken.IsCancellationRequested)
            {
                SpawnUFO();
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnUFOInterval), cancellationToken: cancellationToken);
            }
        }

        private void SpawnAsteroid()
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            _spaceObjectFactory.CreateAsteroid(spawnPosition);
        }

        private void SpawnUFO()
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            _ufoFactory.CreateUFO(spawnPosition);
        }

        private Vector2 GetRandomSpawnPosition()
        {
            _cameraBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
            return -_cameraBounds;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}
