using UnityEngine;

public class SimplePlayerShooter : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public Transform firePoint;
    public GameObject projectilePrefab;

    [Header("Shooting")]
    public float shootCooldown = 0.2f;
    public float rayDistance = 200f;
    public LayerMask hitMask = ~0;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shootClip;

    private float nextShootTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextShootTime)
        {
            Fire();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    private void Fire()
    {
        if (playerCamera == null || firePoint == null || projectilePrefab == null)
        {
            Debug.LogWarning("Assign the camera, fire point, and projectile prefab in the inspector.");
            return;
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, hitMask))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction * rayDistance;
        }

        Vector3 direction = (targetPoint - firePoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
        projectile.transform.forward = direction;

        if (audioSource != null && shootClip != null)
        {
            audioSource.PlayOneShot(shootClip);
        }
    }
}