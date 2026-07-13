using UnityEngine;


[System.Serializable]
public struct ResourceCost
{
    public OreDataSO ore;
    public int amount;
}
public abstract class UpgradeDataSO : ScriptableObject
{
    public string upgradeName;
    public ResourceCost[] costs;
    //UP THE COST EVERYTIME PLAYER BUYS UPGRADE
    public int currentLevel = 0;
    public float costMultiplierPerLevel = 1.5f;


    private void OnEnable()
    {
        currentLevel = 0;
    }

    public int GetCurrentCost(int baseCostAmount)
    {
        //BaseCost * (Multiplier ^ CurrentLevel)
        float multipliedCost = baseCostAmount * Mathf.Pow(costMultiplierPerLevel, currentLevel);
        return Mathf.RoundToInt(multipliedCost);
    }

    public abstract void ApplyUpgrade(GameObject player, GameObject gun);
}
