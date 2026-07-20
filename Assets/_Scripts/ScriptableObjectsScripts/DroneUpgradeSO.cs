using UnityEngine;
[CreateAssetMenu(fileName = "Drone Upgrade", menuName = "Loot System/Upgrades/Drone Upgrade")]
public class DroneUpgradeSO : UpgradeDataSO
{
    [SerializeField] private GameObject dronePrefab;
    [SerializeField] private float DamageIncreased = 20f;
    private Drone droneStats;

    public override void ApplyUpgrade(GameObject player, GameObject gun, GameObject drone)
    {
        //SPAWN DRONE
        GameObject activeDrone = drone;

        Vector3 offset = new Vector3(Random.Range(-50f, 50f), 3f, Random.Range(-20f, 20f));
        Vector3 spawnPosition = Vector3.zero + offset;


        GameObject newDrone = Instantiate(dronePrefab, spawnPosition, dronePrefab.transform.rotation);
        //INCREASE DAMAGE
        Drone droneStats = activeDrone.GetComponent<Drone>();
        droneStats.droneDamagePerSecond += DamageIncreased;
    }
}
