using Unity.Cinemachine;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private GameObject lootPrefab;
    [SerializeField] private int minLoot = 2;
    [SerializeField] private int maxLoot = 5;
    [SerializeField] private LootType oreType;
    private Damageable damageable;

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }
    private void OnEnable()
    {
        damageable.OnDeath += SpawnLoot;
    }
    private void OnDisable()
    {
        damageable.OnDeath -= SpawnLoot;
    }

    private void SpawnLoot()
    {
        int amountToSpawn = Random.Range(minLoot, maxLoot);
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 spawnPosition = transform.position + Vector3.up;
            LootPool.Instance.DropLootAtLocation(oreType, spawnPosition);
        }
        Destroy(gameObject);
    }
}

