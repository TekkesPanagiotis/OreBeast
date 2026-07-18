using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI amountText;

    public void Initialize(OreDataSO ore, int amount)
    {
        iconImage.sprite = ore.oreSprite;
        amountText.text = amount.ToString();
    }

   
    public void UpdateAmount(int amount)
    {
        amountText.text = amount.ToString();
    }
}
