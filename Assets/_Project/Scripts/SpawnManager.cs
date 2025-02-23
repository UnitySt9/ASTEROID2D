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
        
        private Camera _camera;
        private UFOFactory _factory;
        
        private void Start()
        {
            _factory = new UFOFactory(_ufoPrefab);
            _camera = Camera.main;
            StartCoroutine(SpawnAsteroids());
            StartCoroutine(SpawnUFOs());
        }
        
        private IEnumerator SpawnAsteroids()
        {
            while (true)
            {
                SpawnAsteroid();
                yield return new WaitForSeconds(_spawnAsteroidInterval);
            }
        }
        
        private IEnumerator SpawnUFOs()
        {
            while (true)
            {
                SpawnUFO();
                yield return new WaitForSeconds(_spawnUFOInterval);
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
            Vector3 cameraBounds = _camera!.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
            return -cameraBounds;
        }
    }
}
