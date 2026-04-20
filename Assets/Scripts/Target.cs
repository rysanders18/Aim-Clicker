using UnityEngine;

public class Target : MonoBehaviour
{
    public float lifeTime = 3f; 
    private bool wasHit = false;

    private void Start()
    {
        Invoke(nameof(AutoDestroy), lifeTime);
    }

    public void Hit()
    {
        wasHit = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddMoney(GameManager.Instance.shotReward);
        }

        Destroy(gameObject);
    }

    private void AutoDestroy()
    {
        if (wasHit) return;

        Debug.Log("Target expired");

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (TargetSpawner.Instance != null)
        {
            TargetSpawner.Instance.OnTargetDestroyed();
        }
    }
}