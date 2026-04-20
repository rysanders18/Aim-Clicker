using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnEntry
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float weight = 0.14f;
    }

    public SpawnEntry[] targets;
    public Transform[] spawnPoints;
    public float spawnInterval = 1.5f;
    public int maxAliveTargets = 11;

    private int aliveTargets;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (aliveTargets >= maxAliveTargets || targets.Length == 0 || spawnPoints.Length == 0)
                continue;

            GameObject prefab = GetWeightedRandomPrefab();
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject newTarget = Instantiate(prefab, point.position, point.rotation);
            aliveTargets++;

            TargetDeathWatcher watcher = newTarget.AddComponent<TargetDeathWatcher>();
            watcher.spawner = this;
        }
    }

    private GameObject GetWeightedRandomPrefab()
    {
        float totalWeight = 0f;
        foreach (var entry in targets)
        {
            totalWeight += entry.weight;
        }

        float randomValue = Random.value * totalWeight;
        float cumulative = 0f;

        foreach (var entry in targets)
        {
            cumulative += entry.weight;
            if (randomValue <= cumulative)
            {
                return entry.prefab;
            }
        }

        return targets[targets.Length - 1].prefab;
    }

    public void NotifyTargetDestroyed()
    {
        aliveTargets = Mathf.Max(0, aliveTargets - 1);
    }

    private class TargetDeathWatcher : MonoBehaviour
    {
        public TargetSpawner spawner;

        private void OnDestroy()
        {
            if (spawner != null)
            {
                spawner.NotifyTargetDestroyed();
            }
        }
    }
}