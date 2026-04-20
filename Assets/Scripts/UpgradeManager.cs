using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Passive Income Units
    public int gruntCost = 10;
    public int eliteCost = 100;
    public int generalCost = 1000;
    public int warlordCost = 10000;

    public int gruntIncome = 1;
    public int eliteIncome = 10;
    public int generalIncome = 100;
    public int warlordIncome = 1000;

    // Owned Counts
    public int gruntOwned = 0;
    public int eliteOwned = 0;
    public int generalOwned = 0;
    public int warlordOwned = 0;

    // Shot Reward Upgrade
    public int shotRewardUpgradeCost = 15;
    public int shotRewardGain = 1;

    // Button Text References
    public TextMeshProUGUI gruntButtonText;
    public TextMeshProUGUI eliteButtonText;
    public TextMeshProUGUI generalButtonText;
    public TextMeshProUGUI warlordButtonText;
    public TextMeshProUGUI shotRewardButtonText;

    // Owned Count Text References
    public TextMeshProUGUI gruntCountText;
    public TextMeshProUGUI eliteCountText;
    public TextMeshProUGUI generalCountText;
    public TextMeshProUGUI warlordCountText;

    private void Start()
    {
        UpdateAllUI();
    }

    private void UpdateAllUI()
    {
        UpdateGruntButtonText();
        UpdateEliteButtonText();
        UpdateGeneralButtonText();
        UpdateWarlordButtonText();
        UpdateShotRewardButtonText();

        UpdateGruntCountText();
        UpdateEliteCountText();
        UpdateGeneralCountText();
        UpdateWarlordCountText();
    }

    public void BuyGrunt()
    {
        if (TryBuyPassiveUnit(ref gruntCost, gruntIncome, ref gruntOwned, "Bought Grunt"))
        {
            UpdateGruntButtonText();
            UpdateGruntCountText();
        }
    }

    public void BuyElite()
    {
        if (TryBuyPassiveUnit(ref eliteCost, eliteIncome, ref eliteOwned, "Bought Elite"))
        {
            UpdateEliteButtonText();
            UpdateEliteCountText();
        }
    }

    public void BuyGeneral()
    {
        if (TryBuyPassiveUnit(ref generalCost, generalIncome, ref generalOwned, "Bought General"))
        {
            UpdateGeneralButtonText();
            UpdateGeneralCountText();
        }
    }

    public void BuyWarlord()
    {
        if (TryBuyPassiveUnit(ref warlordCost, warlordIncome, ref warlordOwned, "Bought Warlord"))
        {
            UpdateWarlordButtonText();
            UpdateWarlordCountText();
        }
    }

    private bool TryBuyPassiveUnit(ref int cost, int income, ref int ownedCount, string debugMessage)
    {
        if (GameManager.Instance == null)
        {
            return false;
        }

        bool success = GameManager.Instance.SpendMoney(cost);

        if (success)
        {
            GameManager.Instance.AddPassiveIncome(income);
            ownedCount += 1;
            cost = GetNextCost(cost);
            Debug.Log(debugMessage);
            return true;
        }
        else
        {
            Debug.Log("Not enough money");
            return false;
        }
    }

    public void BuyShotRewardUpgrade()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        bool success = GameManager.Instance.SpendMoney(shotRewardUpgradeCost);

        if (success)
        {
            GameManager.Instance.IncreaseShotReward(shotRewardGain);
            shotRewardUpgradeCost = GetNextCost(shotRewardUpgradeCost);
            UpdateShotRewardButtonText();
            Debug.Log("Bought Shot Reward Upgrade");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    private int GetNextCost(int currentCost)
    {
        return Mathf.RoundToInt(currentCost * 1.5f);
    }

    private string FormatCost(int value)
    {
        if (value >= 1000000000)
        {
            return (value / 1000000000f).ToString("0.#") + "B";
        }
        if (value >= 1000000)
        {
            return (value / 1000000f).ToString("0.#") + "M";
        }
        if (value >= 1000)
        {
            return (value / 1000f).ToString("0.#") + "K";
        }

        return value.ToString();
    }

    private void UpdateGruntButtonText()
    {
        if (gruntButtonText != null)
        {
            gruntButtonText.text = "Grunt ($" + FormatCost(gruntCost) + ")";
        }
    }

    private void UpdateEliteButtonText()
    {
        if (eliteButtonText != null)
        {
            eliteButtonText.text = "Elite ($" + FormatCost(eliteCost) + ")";
        }
    }

    private void UpdateGeneralButtonText()
    {
        if (generalButtonText != null)
        {
            generalButtonText.text = "General ($" + FormatCost(generalCost) + ")";
        }
    }

    private void UpdateWarlordButtonText()
    {
        if (warlordButtonText != null)
        {
            warlordButtonText.text = "Warlord ($" + FormatCost(warlordCost) + ")";
        }
    }

    private void UpdateShotRewardButtonText()
    {
        if (shotRewardButtonText != null)
        {
            shotRewardButtonText.text = "Damage Upgrade ($" + FormatCost(shotRewardUpgradeCost) + ")";
        }
    }

    private void UpdateGruntCountText()
    {
        if (gruntCountText != null)
        {
            gruntCountText.text = "x" + gruntOwned;
        }
    }

    private void UpdateEliteCountText()
    {
        if (eliteCountText != null)
        {
            eliteCountText.text = "x" + eliteOwned;
        }
    }

    private void UpdateGeneralCountText()
    {
        if (generalCountText != null)
        {
            generalCountText.text = "x" + generalOwned;
        }
    }

    private void UpdateWarlordCountText()
    {
        if (warlordCountText != null)
        {
            warlordCountText.text = "x" + warlordOwned;
        }
    }
}