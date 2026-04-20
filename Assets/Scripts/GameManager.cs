using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 0;
    public int passiveIncome = 0;
    public int shotReward = 1;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI passiveIncomeText;

    private float passiveTimer = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateMoneyUI();
        UpdatePassiveIncomeUI();
    }

    private void Update()
    {
        passiveTimer += Time.deltaTime;

        if (passiveTimer >= 1f)
        {
            passiveTimer = 0f;
            AddMoney(passiveIncome);
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateMoneyUI();
            return true;
        }

        return false;
    }

    public void AddPassiveIncome(int amount)
    {
        passiveIncome += amount;
        UpdatePassiveIncomeUI();
    }

    public void IncreaseShotReward(int amount)
    {
        shotReward += amount;
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money;
        }
    }

    private void UpdatePassiveIncomeUI()
    {
        if (passiveIncomeText != null)
        {
            passiveIncomeText.text = "Passive Income: " + passiveIncome + "/s";
        }
    }
}