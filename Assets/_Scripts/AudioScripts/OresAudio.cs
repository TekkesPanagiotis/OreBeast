using UnityEngine;

public class OresAudio : MonoBehaviour
{

    
    private Damageable damageable;
    [SerializeField] private AudioClip oreBreakSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        damageable = GetComponent<Damageable>();
    }

    private void OnEnable()
    {
        damageable.OnDeath += damageable_OnDeath;
    }

    private void OnDisable()
    {
        damageable.OnDeath -= damageable_OnDeath;
    }

    private void damageable_OnDeath()
    {
        Play3DSound(oreBreakSound, transform.position);
    }
    private void Play3DSound(AudioClip clip, Vector3 spawnPosition)
    {

        TempAudioPool.Instance.PlaySoundAtLocation(oreBreakSound, transform.position);
    }
}
