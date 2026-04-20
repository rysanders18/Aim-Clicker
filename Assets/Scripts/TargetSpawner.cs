using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner Instance;

    public GameObject targetPrefab;

    // Spawn area
    public float minX = -4f;
    public float maxX = 4f;
    public float minY = 1f;
    public float maxY = 4f;
    public float minZ = 5f;
    public float maxZ = 13f;

    // Target count control
    public int maxTargets = 1;
    private int currentTargetCount = 0;

    // Golden target control
    public bool goldenTargetUnlocked = false;
    [Range(0f, 1f)]
    public float goldenTargetChance = 0.07f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FillTargets();
    }

    public void FillTargets()
    {
        while (currentTargetCount < maxTargets)
        {
            SpawnOneTarget();
        }
    }

    private void SpawnOneTarget()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);

        Vector3 spawnPos = new Vector3(x, y, z);

        GameObject newTarget = Instantiate(targetPrefab, spawnPos, Quaternion.identity);
        currentTargetCount++;

        if (Camera.main != null)
        {
            newTarget.transform.LookAt(Camera.main.transform);
        }

        Target targetScript = newTarget.GetComponent<Target>();
        if (targetScript != null && goldenTargetUnlocked)
        {
            if (Random.value < goldenTargetChance)
            {
                targetScript.isGolden = true;

                Renderer targetRenderer = newTarget.GetComponent<Renderer>();
                if (targetRenderer != null)
                {
                    targetRenderer.material.color = Color.yellow;
                }
            }
        }
    }

    public void OnTargetDestroyed()
    {
        currentTargetCount--;

        if (currentTargetCount < 0)
        {
            currentTargetCount = 0;
        }

        FillTargets();
    }

    public void UnlockDualTarget()
    {
        maxTargets = 2;
        FillTargets();
        Debug.Log("Dual Target Upgrade unlocked");
    }

    public void UnlockGoldenTarget()
    {
        goldenTargetUnlocked = true;
        Debug.Log("Golden Target Upgrade unlocked");
    }
}
