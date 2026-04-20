using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner Instance;

    public GameObject targetPrefab;

    public float minX = -4f;
    public float maxX = 4f;

    public float minY = 1f;
    public float maxY = 4f;

    public float minZ = 5f;
    public float maxZ = 13f;

    private GameObject currentTarget;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        if (currentTarget != null)
        {
            return;
        }

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);

        Vector3 spawnPos = new Vector3(x, y, z);

        currentTarget = Instantiate(targetPrefab, spawnPos, Quaternion.identity);

        if (Camera.main != null)
        {
            currentTarget.transform.LookAt(Camera.main.transform);
        }
    }

    public void OnTargetDestroyed()
    {
        currentTarget = null;
        SpawnTarget();
    }
}
