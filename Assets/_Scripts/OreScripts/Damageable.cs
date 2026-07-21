using System;
using Unity.VisualScripting;
using UnityEngine;

public class Damageable : MonoBehaviour
{
     public float maxHealth;
     public float currentHealth;
    private bool isDead = false;
    public event Action OnDeath;


    //CHANGE ORES COLOR SO IT LOOKS LIKE ITS MELTING
    [Header("Visual Feedback: Melting")]
    private MeshRenderer meshRenderer;
    [SerializeField] private Color hotColor = Color.red; 
    private Color originalColor;
    private Material material;


    //SHRINK ORE ON IMPACT WITH LASER
    [Header("Visual Feedback: Shrinking")]
    private Vector3 originalScale;
    private float minimumSize = 0.7f;

    //MAKE THE OBJECT SHAKE
    [Header("Visual Feedback: Jitter")]
    [SerializeField] private float jitterIntensity = 0.05f;
    private Vector3 originalPosition;
    private float lastDamageTime;

    [Header("Damage UI settings")]
    private float accumulatedDamage = 0f;
    private float lastNumberSpawnTime = 0f;
    private float numberSpawnInterval = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        originalColor = material.color;
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastDamageTime + 0.1f)
        {
            transform.position = originalPosition;
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        accumulatedDamage += damage;
        if (Time.time >= lastNumberSpawnTime + numberSpawnInterval)
        {
           
            DamageNumberPool.Instance.SpawnDamageNumber(accumulatedDamage, transform.position);

            
            accumulatedDamage = 0f;
            lastNumberSpawnTime = Time.time;
        }
        //CHANGE MATERIAL TO HOT
        float healthPercent = currentHealth / maxHealth;
        material.color = Color.Lerp(hotColor, originalColor, healthPercent);
        //MAKE THE ORE SMALLER
        Vector3 shrinkScale = originalScale * minimumSize;
        transform.localScale =Vector3.Lerp(shrinkScale, originalScale, healthPercent);
        //MAKE THE ORE SHAKE
        lastDamageTime = Time.time;
        transform.position = originalPosition + UnityEngine.Random.insideUnitSphere * jitterIntensity;


        if (isDead) return;
        if (currentHealth <= 0) 
        {
            Die();
        }
    }
    private void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
    }
}
