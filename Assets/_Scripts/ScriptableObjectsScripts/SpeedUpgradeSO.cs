using UnityEngine;

[CreateAssetMenu(fileName = "New Speed Upgrade", menuName = "Loot System/Upgrades/Speed Upgrade")]
public class SpeedUpgradeSO : UpgradeDataSO
{
    [SerializeField] private float moveSpeedIncrease = 4f;
    private PlayerMovement playerMovement;
    public override void ApplyUpgrade(GameObject player, GameObject gun)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.moveSpeed += moveSpeedIncrease;
    }
}
