using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;

    public float spawnInterval = 1.5f;
    public float spawnRangeX = 5f;
    public float spawnRangeY = 3f;
    public float spawnDistance = 10f;

    void Start()
    {
        InvokeRepeating("SpawnTarget", 1f, spawnInterval);
    }

    void SpawnTarget()
    {
        float x = Random.Range(-spawnRangeX, spawnRangeX);
        float y = Random.Range(-spawnRangeY, spawnRangeY);

        Vector3 spawnPos = new Vector3(x, y, spawnDistance);

        Instantiate(targetPrefab, spawnPos, Quaternion.identity);
    }
}
