using Unity.Cinemachine;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private GameObject lootPrefab;
    [SerializeField] private CinemachineImpulseSource screenShake;
    [SerializeField] private int minLoot = 2;
    [SerializeField] private int maxLoot = 5;
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
            Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
        }
        screenShake.GenerateImpulse();
        Destroy(gameObject);
    }
}

