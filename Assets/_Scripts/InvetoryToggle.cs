using UnityEngine;

public class InvetoryToggle : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUIPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryUIPanel.SetActive(false);
        GameInputs.Instance.OnInventoryToggle += InventorySwitch;
    }
    private void InventorySwitch()
    {
        ToggleInventory();
    }
    private void ToggleInventory()
    {
        inventoryUIPanel.SetActive(!inventoryUIPanel.activeSelf);
    }
    private void OnDestroy()
    {
        if (GameInputs.Instance != null)
        {
            GameInputs.Instance.OnInventoryToggle -= ToggleInventory;
        }
    }
}
