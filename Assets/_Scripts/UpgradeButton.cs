using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [Header("Upgrade Data")]
    private UpgradeDataSO upgradeData;
    private GameObject player;
    private GameObject gun;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Transform costContainer;
    [SerializeField] private GameObject costSlotPrefab;

    private List<CostSlotUI> spawnedCosts = new List<CostSlotUI>();

    public void Initialize(UpgradeDataSO data, GameObject playerRef, GameObject gunRef)
    {
        upgradeData = data;
        player = playerRef; 
        gun = gunRef;
        upgradeNameText.text = upgradeData.upgradeName;


        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(TryBuyUpgrade);


        foreach (var cost in upgradeData.costs)
        {
            GameObject newCostObj = Instantiate(costSlotPrefab, costContainer, false);
            CostSlotUI costScript = newCostObj.GetComponent<CostSlotUI>();
            costScript.Setup(cost,upgradeData);
            spawnedCosts.Add(costScript);
        }
        RefreshAllUI();
    }

    private void OnEnable()
    {  
      if (PlayerInventory.Instance != null)
          PlayerInventory.Instance.OnInventoryUpdate += OnInventoryChanged;

        RefreshAllUI();
    }

    private void OnDisable()
    {
        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.OnInventoryUpdate -= OnInventoryChanged;
    }

    
    private void OnInventoryChanged(OreDataSO ore, int amount)
    {
        RefreshAllUI();
    }

    private void RefreshAllUI()
    {
        if (upgradeData.IsMaxLevel())
        {
            buyButton.interactable = false; // Grays out the button!
            upgradeNameText.text = $"{upgradeData.upgradeName} (MAX)";
             costContainer.gameObject.SetActive(false); 
        }
        else
        {
            buyButton.interactable = true;
            upgradeNameText.text = $"{upgradeData.upgradeName} Lvl {upgradeData.currentLevel}";
            costContainer.gameObject.SetActive(true);
        }
        foreach (var costSlot in spawnedCosts)
        {
            costSlot.RefreshUI();
        }
    }

    private void TryBuyUpgrade()
    {
        if (upgradeData.IsMaxLevel())
        {
            Debug.Log("Already at max level!");
            return;
        }

        foreach (var cost in upgradeData.costs)
        {
            int actualCost = upgradeData.GetCurrentCost(cost.amount);
            if (!PlayerInventory.Instance.HasEnoughOres(cost.ore, actualCost))
            {
                Debug.Log($"Not enough {cost.ore.oreName}!");
                return;
            }
        }

        
        foreach (var cost in upgradeData.costs)
        {
            int actualCost = upgradeData.GetCurrentCost(cost.amount);
            PlayerInventory.Instance.RemoveOres(cost.ore, actualCost);
        }

        Debug.Log($"Bought {upgradeData.upgradeName}!");
        upgradeData.ApplyUpgrade(player, gun);
        upgradeData.currentLevel++;

        RefreshAllUI();

    }
}