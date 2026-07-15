using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class TempAudioPool : MonoBehaviour
{
    
    public static TempAudioPool Instance { get; private set; }

    [SerializeField] private AudioSource audioPrefab;
    private IObjectPool<AudioSource> pool;

    private void Awake()
    {
        Instance = this;

       
        pool = new ObjectPool<AudioSource>(
            createFunc: CreateAudioSource,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnToPool,
            actionOnDestroy: OnDestroyAudioSource,
            defaultCapacity: 10,
            maxSize: 30 
        );
    }

    private AudioSource CreateAudioSource()
    {
        return Instantiate(audioPrefab);
    }

    private void OnTakeFromPool(AudioSource source)
    {
        source.gameObject.SetActive(true);
    }

    private void OnReturnToPool(AudioSource source)
    {
        source.gameObject.SetActive(false);
    }

    private void OnDestroyAudioSource(AudioSource source)
    {
        Destroy(source.gameObject);
    }

 
    public void PlaySoundAtLocation(AudioClip clip, Vector3 position)
    {
        
        AudioSource source = pool.Get();

        source.transform.position = position;
        source.clip = clip;
        source.Play();

        StartCoroutine(ReturnToPoolAfterPlaying(source));
    }

    private IEnumerator ReturnToPoolAfterPlaying(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        pool.Release(source); 
    }
}
