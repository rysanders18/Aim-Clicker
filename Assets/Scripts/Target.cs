using UnityEngine;

public class Target : MonoBehaviour
{
    public int reward = 1;
    public float lifeTime = 3f; 

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Hit()
    {
        Debug.Log("Target was hit");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddMoney(reward);
        }

        Destroy(gameObject);
    }
}