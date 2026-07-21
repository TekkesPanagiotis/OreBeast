using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    private Damageable damageable;

    private void Start()
    {
        
        damageable = GetComponent<Damageable>();

        if (healthSlider != null && damageable != null)
        {
            healthSlider.maxValue = damageable.maxHealth;
            healthSlider.value = damageable.currentHealth;
        }
    }

    private void Update()
    {
        
        if (healthSlider != null && damageable != null)
        {
            healthSlider.value = damageable.currentHealth;
        }
    }
}
