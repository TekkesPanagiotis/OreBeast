using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class ChallengTeleporter : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    private BoxCollider collider;
    [SerializeField] private GrappleController grappleController;
    [SerializeField] private GameObject lights;
    private bool isTeleported = false;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if(isTeleported == true || grappleController.isGrappleUnlocked == false)
        {
            collider.isTrigger = false;
        }
        else
        {
            collider.isTrigger = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isTeleported)
        {
            if (grappleController.isGrappleUnlocked)
            {
                other.gameObject.transform.position = targetPosition.position;
                lights.SetActive(false);
                isTeleported = true;
            }
        }
    }
}
