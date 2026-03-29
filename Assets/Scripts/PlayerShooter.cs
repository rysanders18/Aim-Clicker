using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);

                Target target = hit.collider.GetComponent<Target>();
                if (target != null)
                {
                    target.Hit();
                }
            }
        }
    }
}
