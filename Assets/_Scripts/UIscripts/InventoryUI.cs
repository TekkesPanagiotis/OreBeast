using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform inventoryPanel;
    private Dictionary<OreDataSO, InventorySlotUI> slotDictionary = new Dictionary<OreDataSO, InventorySlotUI>();

    void Start()
    {
        PlayerInventory.Instance.OnInventoryUpdate += UpdateUI;
    }
    private void OnDestroy()
    {
        PlayerInventory.Instance.OnInventoryUpdate -= UpdateUI;
    }

   private void UpdateUI(OreDataSO ore,int amount)
    {
        //UPDATE NUMBER IF WE HAVE ORE
        if (slotDictionary.ContainsKey(ore))
        {
            slotDictionary[ore].UpdateAmount(amount);
        }
        //SPAWN TEXT IF WE DONT HAVE ORE
        else
        {
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel);

            
            InventorySlotUI slotScript = newSlot.GetComponent<InventorySlotUI>();
            slotScript.Initialize(ore, amount);

            
            slotDictionary.Add(ore, slotScript);
        }
    }

}
