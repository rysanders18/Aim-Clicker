using UnityEngine;

public class MonsterTarget : MonoBehaviour
{
    [Header("Target Settings")]
    

    
    public int points = 10;
    public float maxHealth = 1f;
    public float lifeTime = 8f;
    public bool destroyOnHit = true;

    [Header("Motion")]
    public bool bobUpAndDown = true;
    public float bobSpeed = 2f;
    public float bobHeight = 0.25f;
    public bool rotate = true;
    public float rotateSpeed = 45f;

    [Header("Effects")]
    public ParticleSystem hitEffect;
    public AudioSource hitAudio;
    public AudioClip hitClip;
    public float destroyDelay = 0.5f;

    private float currentHealth;
    private Vector3 startPos;
    private float spawnTime;
    private bool isDead;
    private Collider[] allColliders;
    private Renderer[] allRenderers;

    private void Start()
    {
        currentHealth = maxHealth;
        startPos = transform.position;
        spawnTime = Time.time;

        allColliders = GetComponentsInChildren<Collider>();
        allRenderers = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        if (isDead) return;

        if (bobUpAndDown)
        {
            float yOffset = Mathf.Sin((Time.time - spawnTime) * bobSpeed) * bobHeight;
            transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
        }

        if (rotate)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        }

        if (lifeTime > 0f && Time.time - spawnTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void TakeHit(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (hitEffect != null)
          {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
          }

        if (destroyOnHit || currentHealth <= 0f)
        {
            Die();
        }
    }


    private void Die()
    {
        isDead = true;

        GameManager.Instance?.AddScore(points);

        foreach (Collider c in allColliders)
        {
            c.enabled = false;
        }

        foreach (Renderer r in allRenderers)
        {
            r.enabled = false;
        }

        if (hitAudio != null && hitClip != null)
        {
            hitAudio.PlayOneShot(hitClip);
        }

        Destroy(gameObject, destroyDelay);
    }
}