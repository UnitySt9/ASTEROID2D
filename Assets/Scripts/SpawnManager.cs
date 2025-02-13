using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] GameObject ufoPrefab;
    [SerializeField] UFOFactory factory;
    [SerializeField] Transform spaceShipTransform;
    private int _spawnInterval =5;
    private void Start()
    {
        factory = new UFOFactory(ufoPrefab);
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
    void SpawnAsteroid()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnUFO()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        factory.CreateUFO(spawnPosition, spaceShipTransform);
    }
    Vector2 GetRandomSpawnPosition()
    {
        Camera camera = Camera.main;
        Vector3 cameraBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        return -cameraBounds;
    }
}
