using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootPickup : MonoBehaviour
{
    [SerializeField] private float popForce;
    [SerializeField] private float flyForce;
    private Rigidbody rb;
    private Collider collider;
    [SerializeField] private float delayMagnetize;
    private bool isMagnetize = false;
    private Transform playerTarget;
    public OreDataSO oreData;
    private int amount = 1;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        Vector3 RandomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f)).normalized;

        rb.AddForce(RandomDirection * popForce, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitCircle * popForce, ForceMode.Impulse);
        StartCoroutine(WaitForMagnetize());
    }
    private IEnumerator WaitForMagnetize()
    {
        yield return new WaitForSeconds(delayMagnetize);
        playerTarget = PlayerTarget.Instance;
        rb.isKinematic= true;
        rb.useGravity = false;
        collider.isTrigger = true;
        isMagnetize = true;
    }
    private void Update()
    {
        if (!isMagnetize) return;
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, flyForce * Time.deltaTime);
        if(Vector3.Distance(transform.position, playerTarget.position) < 1f)
        {
            CollectLoot();
        }
    }
    private void CollectLoot()
    {
        Debug.Log($"Collected {oreData.oreName}!");
        //ADD LOOT TO INVETORY
        PlayerInventory.Instance.AddItem(oreData, amount);
        Destroy(gameObject);
    }
}
