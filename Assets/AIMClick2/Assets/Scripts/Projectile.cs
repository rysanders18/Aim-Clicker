using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    public float damage = 1f;
    public float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        MonsterTarget target = other.GetComponentInParent<MonsterTarget>();
        if (target != null)
        {
            target.TakeHit(damage);
        }

        Destroy(gameObject);
    }
}