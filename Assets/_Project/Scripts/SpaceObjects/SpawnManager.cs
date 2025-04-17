using System;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class SpawnManager : IGameStateListener, IDisposable
    {
        private readonly int _spawnAsteroidInterval = 5;
        private readonly int _spawnUFOInterval = 4;

        private CancellationTokenSource _cancellationTokenSource;
        private readonly SpaceObjectFactory _spaceObjectFactory;
        private readonly UFOFactory _ufoFactory;
        private readonly GameStateManager _gameStateManager;
        private readonly Camera _mainCamera;
        private Vector3 _cameraBounds;
        private bool _isGameOver = false;

        public SpawnManager(
            SpaceObjectFactory spaceObjectFactory, 
            UFOFactory ufoFactory, 
            GameStateManager gameStateManager,
            Camera mainCamera)
        {
            _spaceObjectFactory = spaceObjectFactory;
            _ufoFactory = ufoFactory;
            _gameStateManager = gameStateManager;
            _mainCamera = mainCamera;
        }

        public void Initialize()
        {
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
        }

        public void OnGameContinue()
        {
            _isGameOver = false;
        }

        private async UniTaskVoid SpawnAsteroidsAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!_isGameOver)
                {
                    SpawnAsteroid();
                }
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnAsteroidInterval), cancellationToken: cancellationToken);
            }
        }

        private async UniTaskVoid SpawnUFOsAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!_isGameOver)
                {
                    SpawnUFO();
                }
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnUFOInterval), cancellationToken: cancellationToken);
            }
        }

        private async UniTask SpawnAsteroid()
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            await _spaceObjectFactory.CreateAsteroid(spawnPosition);
        }

        private async UniTask SpawnUFO()
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            await _ufoFactory.CreateUFO(spawnPosition);
        }

        private Vector2 GetRandomSpawnPosition()
        {
            float cameraHeight = 2f * _mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * _mainCamera.aspect;
            float spawnX, spawnY;
            int side = Random.Range(0, 4);
            switch (side)
            {
                case 0:
                    spawnX = Random.Range(-cameraWidth / 2, cameraWidth / 2);
                    spawnY = cameraHeight / 2 + 1f;
                    break;
                case 1:
                    spawnX = cameraWidth / 2 + 1f;
                    spawnY = Random.Range(-cameraHeight / 2, cameraHeight / 2);
                    break;
                case 2:
                    spawnX = Random.Range(-cameraWidth / 2, cameraWidth / 2);
                    spawnY = -cameraHeight / 2 - 1f;
                    break;
                case 3:
                    spawnX = -cameraWidth / 2 - 1f;
                    spawnY = Random.Range(-cameraHeight / 2, cameraHeight / 2);
                    break;
                default:
                    spawnX = 0;
                    spawnY = 0;
                    break;
            }
            return new Vector2(spawnX, spawnY);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}
