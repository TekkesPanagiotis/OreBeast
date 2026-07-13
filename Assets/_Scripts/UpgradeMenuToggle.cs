using UnityEngine;

public class UpgradeMenuToggle : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenuPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradeMenuPanel.SetActive(false);
        GameInputs.Instance.OnUpgradeMenuToggle += ToggleMenu;
    }

    private void ToggleMenu()
    {
        bool isNowOpen = !upgradeMenuPanel.activeSelf;
        upgradeMenuPanel.SetActive(isNowOpen);

        
        if (isNowOpen)
        {
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false; 
        }
    }
    private void OnDestroy()
    {
        GameInputs.Instance.OnUpgradeMenuToggle -= ToggleMenu;
    }
}
