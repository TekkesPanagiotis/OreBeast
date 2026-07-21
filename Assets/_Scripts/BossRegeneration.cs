using UnityEngine;

public class BossRegeneration : MonoBehaviour
{
    private Damageable damageable;
    private float nextRegeneration;
    [SerializeField] private float timeBetweenRegeneration;
    [SerializeField] private float regenerationValue;

    private void Start()
    {
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        if(damageable.currentHealth < damageable.maxHealth && Time.time >= nextRegeneration)
        {
            Regen();
        }
    }

    private void Regen()
    {
        damageable.currentHealth += regenerationValue;
        if (damageable.currentHealth > damageable.maxHealth)
        {
            damageable.currentHealth = damageable.maxHealth;
        }
        nextRegeneration = Time.time + timeBetweenRegeneration;
    }
}
