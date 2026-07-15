using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum LootType
{
    rock,
    gold,
    Sapphire,
}

[System.Serializable]
public struct LootPoolSetup
{
    public LootType lootType;
    public GameObject lootPrefab;
}
public class LootPool : MonoBehaviour
{
    public static LootPool Instance { get; private set; }
    [SerializeField] private List<LootPoolSetup> lootSetups = new List<LootPoolSetup>();
    private Dictionary<LootType, IObjectPool<GameObject>> poolDictionary = new Dictionary<LootType, IObjectPool<GameObject>>();
    private Dictionary<LootType, GameObject> prefabDictionary = new Dictionary<LootType, GameObject>();

    private void Awake()
    {
        Instance = this;
        InitializePools();
    }
    private void InitializePools()
    {
        foreach (var setup in lootSetups)
        {
            // Store the prefab so the pool knows what to instantiate
            prefabDictionary[setup.lootType] = setup.lootPrefab;

            // Create a new pool for this specific LootType and add it to our dictionary
            poolDictionary[setup.lootType] = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(prefabDictionary[setup.lootType]),
                actionOnGet: (loot) => loot.SetActive(true),
                actionOnRelease: (loot) => loot.SetActive(false),
                actionOnDestroy: (loot) => Destroy(loot),
                defaultCapacity: 20,
                maxSize: 50
            );
        }
    }
    public void DropLootAtLocation(LootType type, Vector3 position)
    {
        if (poolDictionary.TryGetValue(type, out var pool))
        {
            GameObject droppedLoot = pool.Get();
            droppedLoot.transform.position = position;

            // Make sure the loot knows what type it is so it can be returned correctly!
            if (droppedLoot.TryGetComponent(out PooledLootItem lootItem))
            {
                lootItem.MyType = type;
            }
        }
        else
        {
            Debug.LogError($"No pool found for LootType: {type}! Did you add it to the Inspector?");
        }
    }

    public void ReturnLootToPool(LootType type, GameObject loot)
    {
        if (poolDictionary.TryGetValue(type, out var pool))
        {
            pool.Release(loot);
        }
    }
}
