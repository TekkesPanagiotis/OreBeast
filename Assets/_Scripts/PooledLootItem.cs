using UnityEngine;

public class PooledLootItem : MonoBehaviour
{
    // The Pool Manager will set this when the item drops
    [HideInInspector] public LootType MyType;
}