using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject ufoPrefab;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            int spawnInterval = Random.Range(3, 7);
            SpawnAsteroid();
            SpawnUFO();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    // Спавн астероидов
    private void SpawnAsteroid()
    {
        int spawnDistance = Random.Range(4, 7);
        float randomAngle = Random.Range(0f, 360f);
        Vector2 spawnPosition = player.position + new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 0) * spawnDistance;
        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
    }

    private void SpawnUFO()
    {
        int spawnDistance = Random.Range(4, 7);
        float randomAngle = Random.Range(0f, 360f);
        Vector2 spawnPosition = player.position + new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 0) * spawnDistance;
        Instantiate(ufoPrefab, spawnPosition, Quaternion.identity);
    }
}
