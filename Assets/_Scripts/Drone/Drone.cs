using Unity.VisualScripting;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float hoverHeight;
    [SerializeField] private float detectionRadius;
     public float droneDamagePerSecond;
    [SerializeField] private LayerMask oreLayer;

    //VISUAL
    [Header("Visuals")]
    [SerializeField] private LineRenderer laserVisual;
    [SerializeField] private Transform laserShootPoint;

    private Damageable currentTarget;

    //SOUND
    [Header("SOUNDS")]
    [SerializeField] private AudioClip laserClip;
    [SerializeField] private float soundInterval = 0.8f;
    private float soundTimer;

    [Header("ANIMATIONS")]
    [SerializeField] private Animator animator;
   
    void Start()
    {
        laserVisual.enabled = false;
    }

    
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
        animator.SetBool("IsWalking", true);
        if(Vector3.Distance(transform.position, targetHoverPosition) < 0.2f)
        {
            animator.SetBool("IsWalking", false);
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

        currentTarget.TakeDamage(droneDamagePerSecond * Time.deltaTime);
        //LASER SOUND 
        soundTimer -= Time.deltaTime;
        if (soundTimer <= 0f)
        {
            TempAudioPool.Instance.PlaySoundAtLocation(laserClip, transform.position);
            soundTimer = soundInterval;
        }
    }
    private void StopMining()
    {
        laserVisual.enabled = false;
        soundTimer = 0f;
    }

    public float DroneDamage()
    {
        return droneDamagePerSecond;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
