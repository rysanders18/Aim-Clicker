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

    private bool goldenBuffActive = false;
    private float goldenBuffTimer = 0f;
    public float goldenBuffDuration = 7f;
    public int goldenBuffMultiplier = 7;

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

            int incomeToAdd = passiveIncome;

            if (goldenBuffActive)
            {
                incomeToAdd *= goldenBuffMultiplier;
            }

            AddMoney(incomeToAdd);
        }

        if (goldenBuffActive)
        {
            goldenBuffTimer -= Time.deltaTime;

            if (goldenBuffTimer <= 0f)
            {
                goldenBuffActive = false;
                goldenBuffTimer = 0f;
                UpdatePassiveIncomeUI();
            }
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

    public void ActivateGoldenBuff()
    {
        goldenBuffActive = true;
        goldenBuffTimer = goldenBuffDuration;
        UpdatePassiveIncomeUI();
        Debug.Log("Golden buff activated!");
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
            if (goldenBuffActive)
            {
                passiveIncomeText.text = "Passive Income: " + passiveIncome + "/s  (x" + goldenBuffMultiplier + ")";
            }
            else
            {
                passiveIncomeText.text = "Passive Income: " + passiveIncome + "/s";
            }
        }
    }
}