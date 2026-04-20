using UnityEngine;

public class Target : MonoBehaviour
{
    public float lifeTime = 3f;
    public bool isGolden = false;

    public AudioClip hitSound;

    private bool wasHit = false;
    private bool isBeingDestroyed = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke(nameof(AutoDestroy), lifeTime);
    }

    public void Hit()
    {
        if (isBeingDestroyed) return;

        wasHit = true;
        isBeingDestroyed = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddMoney(GameManager.Instance.shotReward);

            if (isGolden)
            {
                GameManager.Instance.ActivateGoldenBuff();
            }
        }

        CancelInvoke(nameof(AutoDestroy));

        Renderer renderer = GetComponent<Renderer>();
        Collider collider = GetComponent<Collider>();

        if (renderer != null)
        {
            renderer.enabled = false;
        }

        if (collider != null)
        {
            collider.enabled = false;
        }

        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
            Destroy(gameObject, hitSound.length);
        }
        else
        {
            Destroy(gameObject, 0.05f);
        }
    }

    private void AutoDestroy()
    {
        if (wasHit || isBeingDestroyed) return;

        isBeingDestroyed = true;
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