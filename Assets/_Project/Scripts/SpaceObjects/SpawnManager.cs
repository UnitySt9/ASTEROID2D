using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    public class SpawnManager : IGameStateListener, IDisposable
    {
        private readonly int _spawnAsteroidInterval = 5;
        private readonly int _spawnUFOInterval = 4;

        private SpaceObjectFactory _spaceObjectFactory;
        private UFOFactory _ufoFactory;
        private GameStateManager _gameStateManager;
        private WaitForSeconds _waitForAsteroidSpawn;
        private WaitForSeconds _waitForUFOSpawn;
        private Camera _camera;
        private Vector3 _cameraBounds;
        private Coroutine _asteroidSpawnCoroutine;
        private Coroutine _ufoSpawnCoroutine;
        private bool _isGameOver = false;
        private MonoBehaviour _monoBehaviour;

        public SpawnManager(MonoBehaviour monoBehaviour, SpaceObjectFactory spaceObjectFactory, UFOFactory ufoFactory, GameStateManager gameStateManager)
        {
            _monoBehaviour = monoBehaviour;
            _spaceObjectFactory = spaceObjectFactory;
            _ufoFactory = ufoFactory;
            _gameStateManager = gameStateManager;
        }

        public void Initialize()
        {
            _camera = Camera.main;
            _waitForAsteroidSpawn = new WaitForSeconds(_spawnAsteroidInterval);
            _waitForUFOSpawn = new WaitForSeconds(_spawnUFOInterval);
            StartSpawning();
            _gameStateManager.RegisterListener(this);
        }

        private void StartSpawning()
        {
            _asteroidSpawnCoroutine = _monoBehaviour.StartCoroutine(SpawnAsteroids());
            _ufoSpawnCoroutine = _monoBehaviour.StartCoroutine(SpawnUFOs());
        }

        public void OnGameOver()
        {
            _isGameOver = true;
            if (_asteroidSpawnCoroutine != null)
                _monoBehaviour.StopCoroutine(_asteroidSpawnCoroutine);

            if (_ufoSpawnCoroutine != null)
                _monoBehaviour.StopCoroutine(_ufoSpawnCoroutine);
        }

        private IEnumerator SpawnAsteroids()
        {
            while (!_isGameOver)
            {
                SpawnAsteroid();
                yield return _waitForAsteroidSpawn;
            }
        }

        private IEnumerator SpawnUFOs()
        {
            while (!_isGameOver)
            {
                SpawnUFO();
                yield return _waitForUFOSpawn;
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
            if (_asteroidSpawnCoroutine != null)
                _monoBehaviour.StopCoroutine(_asteroidSpawnCoroutine);

            if (_ufoSpawnCoroutine != null)
                _monoBehaviour.StopCoroutine(_ufoSpawnCoroutine);
        }
    }
}
