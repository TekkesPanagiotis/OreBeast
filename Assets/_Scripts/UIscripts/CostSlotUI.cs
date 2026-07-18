using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostSlotUI : MonoBehaviour
{
    [SerializeField] private Image oreIcon;
    [SerializeField] private TextMeshProUGUI costText;
    private ResourceCost baseCost;
    private UpgradeDataSO parentUpgrade;

    private ResourceCost requiredCost;

    public void Setup(ResourceCost cost, UpgradeDataSO upgradeData)
    {
        baseCost = cost;
        parentUpgrade = upgradeData;
        oreIcon.sprite = cost.ore.oreSprite;
        RefreshUI();
    }


    public void RefreshUI()
    {
        
        int actualCost = parentUpgrade.GetCurrentCost(baseCost.amount);

        
        costText.text = actualCost.ToString();

        
        if (PlayerInventory.Instance.HasEnoughOres(baseCost.ore, actualCost))
        {
            costText.color = Color.green;
        }
        else
        {
            costText.color = Color.red;
        }
    }
}