using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float hoverHeight;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float damagePerSecond;
    [SerializeField] private LayerMask oreLayer;

    //VISUAL
    [Header("Visuals")]
    [SerializeField] private LineRenderer laserVisual;
    [SerializeField] private Transform laserShootPoint;

    private Damageable currentTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laserVisual.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(currentTarget == null)
        {
            StopMining();
            FindNearestOre();
        }
        else
        {
            Mine();
        }
    }
    private void FindNearestOre()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, oreLayer);
        float closestDistance = Mathf.Infinity;
        Damageable closestOre = null;
        foreach (Collider collider in colliders) 
        {
            Damageable potentialTarget = collider.GetComponent<Damageable>();
            if (potentialTarget != null) 
            {
                float distanceToOre = Vector3.Distance(transform.position, collider.transform.position);
                if(distanceToOre < closestDistance)
                {
                    closestDistance = distanceToOre;
                    closestOre = potentialTarget;
                }
            }
        }
        currentTarget = closestOre;
    }

    private void Mine()
    {
        Vector3 targetHoverPosition = currentTarget.transform.position + (Vector3.up * hoverHeight);
        transform.position = Vector3.MoveTowards(transform.position, targetHoverPosition, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, targetHoverPosition) < 0.2f)
        {
            StartMining();
        }
        else
        {
            StopMining();
        }
    }
    private void StartMining()
    {
        laserVisual.enabled = true;
        if (laserVisual.enabled && currentTarget != null)
        {
            laserVisual.SetPosition(0, laserShootPoint.position);
            
            laserVisual.SetPosition(1, currentTarget.transform.position + (Vector3.up * 0.5f));
        }

        currentTarget.TakeDamage(damagePerSecond * Time.deltaTime);
    }
    private void StopMining()
    {
        laserVisual.enabled = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
