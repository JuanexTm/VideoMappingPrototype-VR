using UnityEngine;
using System.Collections;

public class ButterflySpawner : MonoBehaviour
{
    public GameObject butterflyPrefab;
    public int butterflyCount = 20;
    public Vector3 spawnAreaSize = new Vector3(10, 5, 10);
    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 15f;

    void Start()
    {
        StartCoroutine(SpawnButterfliesRandomly());
    }

    IEnumerator SpawnButterfliesRandomly()
    {
        while (true)
        {
            SpawnButterfly();
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnButterfly()
    {
        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        GameObject butterfly = Instantiate(butterflyPrefab, randomPos, Quaternion.identity);
        butterfly.transform.SetParent(this.transform);

        // Si quieres, puedes rotarla al azar
        butterfly.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }
}
