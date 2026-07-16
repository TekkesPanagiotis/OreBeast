using System.Collections;
using UnityEngine;


[System.Serializable]
public class SpawnableOre
{
    public GameObject orePrefab;
    public int spawnWeight = 10;
}

public class OreSpawner : MonoBehaviour
{
    
    [SerializeField] private SpawnableOre[] allowedOres;

    [SerializeField] private float spawnDelay = 10f;
    [SerializeField] private float spawnRadius = 3f;

    private GameObject currentSpawnedOre;
    private bool isSpawning = false;

    void Update()
    {
        if (currentSpawnedOre == null && !isSpawning)
        {
            StartCoroutine(SpawnOreRoutine());
        }
    }

    IEnumerator SpawnOreRoutine()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);

        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            0f,
            Random.Range(-spawnRadius, spawnRadius)
        );
        Vector3 spawnPosition = transform.position + Vector3.up + randomOffset;

        
        GameObject selectedOre = GetRandomOreByWeight();

        
        if (selectedOre != null)
        {
            currentSpawnedOre = Instantiate(selectedOre, spawnPosition, selectedOre.transform.rotation);
        }
        else
        {
            Debug.LogWarning("No ore selected! Check your weights in the Inspector.");
        }

        isSpawning = false;
    }

    //WEIGHT LOGIC
    private GameObject GetRandomOreByWeight()
    {
        
        int totalWeight = 0;
        foreach (SpawnableOre ore in allowedOres)
        {
            totalWeight += ore.spawnWeight;
        }

        
        int randomValue = Random.Range(0, totalWeight);
        int currentWeight = 0;

        
        foreach (SpawnableOre ore in allowedOres)
        {
            currentWeight += ore.spawnWeight;
            if (randomValue < currentWeight)
            {
                return ore.orePrefab;
            }
        }

        return null;     }
}