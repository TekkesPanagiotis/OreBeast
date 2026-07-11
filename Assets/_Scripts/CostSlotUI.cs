using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostSlotUI : MonoBehaviour
{
    [SerializeField] private Image oreIcon;
    [SerializeField] private TextMeshProUGUI costText;

    private ResourceCost requiredCost;

    public void Setup(ResourceCost cost)
    {
        requiredCost = cost;
        oreIcon.sprite = cost.ore.oreSprite;
        costText.text = cost.amount.ToString();
        RefreshColor();
    }

    
    public void RefreshColor()
    {
        if (PlayerInventory.Instance.HasEnoughOres(requiredCost.ore, requiredCost.amount))
        {
            costText.color = Color.green;
        }
        else
        {
            costText.color = Color.red;
        }
    }
}