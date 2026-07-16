using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GrappleController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public bool isGrappleUnlocked = false;
    [SerializeField] private Transform camTransform;
    [SerializeField] private float grappleRange = 50f;
    [SerializeField] private float grappleSpeed = 30f;
    private float nextGrappleTime;
    private float timeBetweenGrapples = 1f;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] LineRenderer grappleVisual;
    [SerializeField] private Transform gunTip;
    [SerializeField] private GameObject grappleText;
    [SerializeField] private Image crosshair;

    private Vector3 grappleTarget;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (grappleVisual != null)
        {
            grappleVisual.enabled = false;
        }
        grappleText.SetActive(false);
    }
    void Start()
    {
        GameInputs.Instance.OnGrappleStarted += TryGrapple;
    }
    private void OnDestroy()
    {
        GameInputs.Instance.OnGrappleStarted -= TryGrapple;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isGrappling && isGrappleUnlocked)
        {
            grappleText.SetActive(false);
            ExecuteGrapple();
        }
        else if (!playerMovement.isGrappling && isGrappleUnlocked)
        {
            CheckForGrappleTarget();
        }
    }
    //THIS FUNCTION SPAWNS THE TEXT TO GRAPPLE
    void CheckForGrappleTarget()
    {

        if (Time.time < nextGrappleTime)
        {
            grappleText.SetActive(false);
            return;
        }


        if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, grappleRange, grappleLayer))
        {
            crosshair.color = Color.green;
            grappleText.SetActive(true);
            grappleText.transform.position = hit.transform.position + (Vector3.up * 2f);
        }
        else
        {
            crosshair.color = Color.white;
            grappleText.SetActive(false);
        }
    }
    void TryGrapple()
    {
        if (playerMovement.isGrappling && isGrappleUnlocked) return;

        if (Time.time < nextGrappleTime && isGrappleUnlocked)
        {
            Debug.Log("Grapple is on cooldown!");
            crosshair.color = Color.white;
            return;
        }

        if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, grappleRange, grappleLayer) && isGrappleUnlocked)
        {
            //GET GRAPPLE POSITION
            grappleTarget = hit.point;
            playerMovement.isGrappling = true;
            //GRAPPLE LINERENDERER & TEXT
            grappleVisual.enabled = true;
            grappleVisual.SetPosition(1, grappleTarget);
            grappleText.SetActive(true);
            nextGrappleTime = Time.time + timeBetweenGrapples;
        }
    }

    void ExecuteGrapple()
    {
        if (grappleVisual != null)
        {
            grappleVisual.SetPosition(0, gunTip.position);
        }
        crosshair.color = Color.white;
        transform.position = Vector3.MoveTowards(transform.position, grappleTarget, grappleSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, grappleTarget) < 1f)
        {
            playerMovement.isGrappling = false;
            grappleVisual.enabled = false;
        }
    }

}