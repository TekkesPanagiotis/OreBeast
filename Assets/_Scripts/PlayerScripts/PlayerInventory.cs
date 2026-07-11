using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    private Dictionary<OreDataSO,int> inventory = new Dictionary<OreDataSO,int>();
    public event Action<OreDataSO, int> OnInventoryUpdate;


    private void Awake()
    {
        Instance = this;
    }
    public void AddItem(OreDataSO ore, int amount)
    {
        if (inventory.ContainsKey(ore))
        {
            inventory[ore] += amount;
        }
        else
        {
            inventory.Add(ore, amount);
        }
        Debug.Log($"Inventory: You now have {inventory[ore]} {ore.oreName}!");
        //USE EVENT FOR UI
        OnInventoryUpdate?.Invoke(ore, inventory[ore]);
    }

    //INVENTORY-UPGRADE SYSTEM
    //DOES THE PLAYER HAS ENOUGH ORES
    public bool HasEnoughOres(OreDataSO ore, int amount)
    {
        if (inventory.ContainsKey(ore))
        {
            return inventory[ore] >= amount;
        }
         return false;
    }

    //WE HAVE ENOUGH SO REMOVE THE ORES AND UPDATE UI
    public void RemoveOres(OreDataSO ore, int amount)
    {
        if (HasEnoughOres(ore, amount))
        {
            inventory[ore] -= amount;
            OnInventoryUpdate?.Invoke(ore, inventory[ore]);
        }
    }
}
