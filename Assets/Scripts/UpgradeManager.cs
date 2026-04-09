using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int autoCollectorCost = 10;
    public int passiveIncomeGain = 1;

    public void BuyAutoCollector()
    {
        if (GameManager.Instance != null)
        {
            bool success = GameManager.Instance.SpendMoney(autoCollectorCost);

            if (success)
            {
                GameManager.Instance.AddPassiveIncome(passiveIncomeGain);
                Debug.Log("Bought Auto Collector");
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
    }
}
