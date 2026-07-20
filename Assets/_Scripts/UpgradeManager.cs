using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private List<UpgradeDataSO> allAvailableUpgrades;

    [Header("UI Setup")]
    [SerializeField] private GameObject upgradeButtonPrefab;
    [SerializeField] private Transform upgradeScrollViewContent;

    [SerializeField] private GameObject playerReference;
    [SerializeField] private GameObject gunReference;
    [SerializeField] private GameObject droneReference;

    private void Start()
    {
        PopulateUpgradeShop();
    }

    private void PopulateUpgradeShop()
    {
       
        foreach (UpgradeDataSO upgradeSO in allAvailableUpgrades)
        {
            
            GameObject newButton = Instantiate(upgradeButtonPrefab, upgradeScrollViewContent, false);

            
            UpgradeButton buttonScript = newButton.GetComponent<UpgradeButton>();
            buttonScript.Initialize(upgradeSO,playerReference,gunReference,droneReference);
        }
    }
}