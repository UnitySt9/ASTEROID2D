using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] GameObject _asteroidPrefab;
        [SerializeField] UFO _ufoPrefab;
        [SerializeField] Transform _spaceShipTransform;
        
        private Camera _camera;
        private UFOFactory _factory;
        private readonly int _spawnInterval =5;
        
        private void Start()
        {
            _factory = new UFOFactory(_ufoPrefab);
            _camera = Camera.main;
            StartCoroutine(SpawnObjects());
        }
        
        private IEnumerator SpawnObjects()
        {
            while (true)
            {
                SpawnAsteroid();
                SpawnUFO();
                yield return new WaitForSeconds(_spawnInterval);
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
