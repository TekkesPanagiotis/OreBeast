using UnityEngine;

[CreateAssetMenu(fileName = "Grapple Upgrade", menuName = "Loot System/Upgrades/Grapple Upgrade")]
public class GrappleUpgradeSO : UpgradeDataSO
{
    private GrappleController grappleController;
    public override void ApplyUpgrade(GameObject player, GameObject gun)
    {
        GrappleController grappleController = player.GetComponent<GrappleController>();
        grappleController.isGrappleUnlocked = true;
    }
}
