using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts
{
    public class SpawnManager : MonoBehaviour, IGameStateListener
    {
        private readonly int _spawnAsteroidInterval =5;
        private readonly int _spawnUFOInterval =4;
        
        [SerializeField] SpaceObjectFactory _spaceObjectFactory;
        [SerializeField] UFOFactory _ufoFactory;
        [SerializeField] GameStateManager _gameStateManager;
        
        private SpaceShipShooting _spaceShip;
        private Transform _spaceShipTransform;
        private WaitForSeconds _waitForAsteroidSpawn;
        private WaitForSeconds _waitForUFOSpawn;
        private Camera _camera;
        private Vector3 _cameraBounds;
        private bool _isGameOver = false;
        
        private void Start()
        {
            _camera = Camera.main;
            _waitForAsteroidSpawn = new WaitForSeconds(_spawnAsteroidInterval);
            _waitForUFOSpawn = new WaitForSeconds(_spawnUFOInterval);
            StartCoroutine(SpawnAsteroids());
            StartCoroutine(SpawnUFOs());
            _gameStateManager.RegisterListener(this);
            _spaceShip = FindObjectOfType<SpaceShipShooting>();
            _spaceShipTransform = _spaceShip.GetComponent<Transform>();
        }
        
        private void OnDestroy()
        {
            _gameStateManager.UnregisterListener(this);
        }
        
        public void OnGameOver()
        {
            _isGameOver = true;
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
            _spaceObjectFactory.CreateAsteroid(spawnPosition, _gameStateManager);
        }

        private void SpawnUFO()
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            _ufoFactory.CreateUFO(spawnPosition, _spaceShipTransform, _gameStateManager);
        }
        
        Vector2 GetRandomSpawnPosition()
        {
            _cameraBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
            return -_cameraBounds;
        }
    }
}
