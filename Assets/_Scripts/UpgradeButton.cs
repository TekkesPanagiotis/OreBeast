using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [Header("Upgrade Data")]
    [SerializeField] private UpgradeDataSO upgradeData;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Transform costContainer;
    [SerializeField] private GameObject costSlotPrefab;

    private List<CostSlotUI> spawnedCosts = new List<CostSlotUI>();

    private void Start()
    {
        upgradeNameText.text = upgradeData.upgradeName;
        buyButton.onClick.AddListener(TryBuyUpgrade);

        
        foreach (var cost in upgradeData.costs)
        {
            GameObject newCostObj = Instantiate(costSlotPrefab, costContainer, false);
            CostSlotUI costScript = newCostObj.GetComponent<CostSlotUI>();
            costScript.Setup(cost);
            spawnedCosts.Add(costScript);
        }
    }

    private void OnEnable()
    {
       
        RefreshAllColors();

       
        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.OnInventoryUpdate += OnInventoryChanged;
    }

    private void OnDisable()
    {
        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.OnInventoryUpdate -= OnInventoryChanged;
    }

    
    private void OnInventoryChanged(OreDataSO ore, int amount)
    {
        RefreshAllColors();
    }

    private void RefreshAllColors()
    {
        foreach (var costSlot in spawnedCosts)
        {
            costSlot.RefreshColor();
        }
    }

    private void TryBuyUpgrade()
    {
        
        foreach (var cost in upgradeData.costs)
        {
            if (!PlayerInventory.Instance.HasEnoughOres(cost.ore, cost.amount))
            {
                Debug.Log($"Not enough {cost.ore.oreName}!");
                return;
            }
        }

        
        foreach (var cost in upgradeData.costs)
        {
            PlayerInventory.Instance.RemoveOres(cost.ore, cost.amount);
        }

        Debug.Log($"Bought {upgradeData.upgradeName}!");
        // Apply your upgrade logic here!
    }
}