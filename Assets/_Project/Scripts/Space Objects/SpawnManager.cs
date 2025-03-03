using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    public class SpawnManager : MonoBehaviour
    {
        private readonly int _spawnAsteroidInterval =5;
        private readonly int _spawnUFOInterval =4;
        
        [SerializeField] GameObject _asteroidPrefab;
        [SerializeField] UFO _ufoPrefab;
        [SerializeField] Transform _spaceShipTransform;
        
        private WaitForSeconds _waitForAsteroidSpawn;
        private WaitForSeconds _waitForUFOSpawn;
        private Camera _camera;
        private Vector3 _cameraBounds;
        private UFOFactory _factory;
        
        private void Start()
        {
            _factory = new UFOFactory(_ufoPrefab);
            _camera = Camera.main;
            _waitForAsteroidSpawn = new WaitForSeconds(_spawnAsteroidInterval);
            _waitForUFOSpawn = new WaitForSeconds(_spawnUFOInterval);
            StartCoroutine(SpawnAsteroids());
            StartCoroutine(SpawnUFOs());
        }
        
        private IEnumerator SpawnAsteroids()
        {
            while (true)
            {
                SpawnAsteroid();
                yield return _waitForAsteroidSpawn;
            }
        }
        
        private IEnumerator SpawnUFOs()
        {
            while (true)
            {
                SpawnUFO();
                yield return _waitForUFOSpawn;
            }
        }
        
        private void SpawnAsteroid()
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            Instantiate(_asteroidPrefab, spawnPosition, Quaternion.identity);
        }

        private void SpawnUFO()
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            _factory.CreateUFO(spawnPosition, _spaceShipTransform);
        }
        
        Vector2 GetRandomSpawnPosition()
        {
            _cameraBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
            return -_cameraBounds;
        }
    }
}
