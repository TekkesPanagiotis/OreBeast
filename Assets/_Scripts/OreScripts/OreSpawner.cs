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

    //AVOID OBSTACLES
    [SerializeField] private LayerMask obstacleLayer;
    private float checkRadius = 1f;
    private int maxSpawnAttempts = 10;

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
        Vector3 spawnPosition = Vector3.zero;
        bool foundValidPosition = false;

        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0f,
                Random.Range(-spawnRadius, spawnRadius)
            );
            Vector3 potentialPosition = transform.position + Vector3.up + randomOffset;

            if (!Physics.CheckSphere(potentialPosition, checkRadius, obstacleLayer))
            {
                spawnPosition = potentialPosition;
                foundValidPosition = true;
                break; //FOUND SPOT SO EXIT
            }
        }
        if (foundValidPosition)
        {
            GameObject selectedOre = GetRandomOreByWeight();
            currentSpawnedOre = Instantiate(selectedOre, spawnPosition, selectedOre.transform.rotation);
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

        return null;     
    }
    private void OnDrawGizmosSelected()
    {
       //GIZMO FOR SPAWN RADIUS
        Vector3 center = transform.position + Vector3.up;

       
        Gizmos.color = Color.green;
        
        Vector3 areaSize = new Vector3(spawnRadius * 2, 0.1f, spawnRadius * 2);
        Gizmos.DrawWireCube(center, areaSize);

        //GIZMO FOR CHECK RADIUS
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, checkRadius);
    }
}