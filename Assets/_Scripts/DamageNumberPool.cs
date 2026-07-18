using UnityEngine;
using UnityEngine.Pool;

public class DamageNumberPool : MonoBehaviour
{
    public static DamageNumberPool Instance { get; private set; }

    [SerializeField] private DamageNumberUI textPrefab;
    private IObjectPool<DamageNumberUI> pool;

    private void Awake()
    {
        Instance = this;

       
        pool = new ObjectPool<DamageNumberUI>(
            createFunc: () => Instantiate(textPrefab),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            defaultCapacity: 20,
            maxSize: 50 
        );
    }

    public void SpawnDamageNumber(float damageAmount, Vector3 spawnPosition)
    {
        DamageNumberUI damageNumUI = pool.Get();

        
        Vector3 randomOffset = new Vector3(
            Random.Range(-0.5f, 0.5f),
            Random.Range(0.5f, 1f),
            Random.Range(-0.5f, 0.5f)
        );

        damageNumUI.transform.position = spawnPosition + randomOffset;

     
        damageNumUI.Setup(damageAmount, (dn) => pool.Release(dn));
    }
}
