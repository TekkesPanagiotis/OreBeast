using Unity.Cinemachine;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private CinemachineImpulseSource CinemachineImpulseSource;
    private Damageable damageable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        damageable = GetComponent<Damageable>();
        CinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnEnable()
    {
        
        if (damageable != null)
        {
            damageable.OnDeath += TriggerShake;
        }
    }

    private void OnDisable()
    {
        
        if (damageable != null)
        {
            damageable.OnDeath -= TriggerShake;
        }
    }

    
    private void TriggerShake()
    {
        if (CinemachineImpulseSource != null)
        {
            CinemachineImpulseSource.GenerateImpulse();
        }
    }
}
