using UnityEngine;

[CreateAssetMenu(fileName = "New Speed Upgrade", menuName = "Loot System/Upgrades/Damage Upgrade")]
public class DamageUpgradeSO : UpgradeDataSO
{
    private GunStats gunStats;
    [SerializeField] private float damageIncreased = 10;

    public override void ApplyUpgrade(GameObject player, GameObject gun)
    {
        GunStats gunStats = gun.GetComponent<GunStats>();
        gunStats.damagePerSecond += damageIncreased;
    }
}
