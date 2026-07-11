using UnityEngine;


[System.Serializable]
public struct ResourceCost
{
    public OreDataSO ore;
    public int amount;
}
[CreateAssetMenu(fileName = "New Upgrade", menuName = "Loot System/Upgrade Data")]
public class UpgradeDataSO : ScriptableObject
{
    public string upgradeName;
    public ResourceCost[] costs;
}
