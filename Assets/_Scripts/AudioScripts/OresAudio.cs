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
       
        GameObject audioObj = new GameObject("Temp3DAudio");
        audioObj.transform.position = spawnPosition;

      
        AudioSource source = audioObj.AddComponent<AudioSource>();

       
        source.clip = clip;
        source.volume = 1f;      
        source.pitch = Random.Range(0.9f, 1.1f); 

     
        source.spatialBlend = 1f;

        source.rolloffMode = AudioRolloffMode.Linear;
        source.minDistance = 5f;
        source.maxDistance = 25f;
        source.Play();
        Destroy(audioObj, clip.length);
    }
}
