using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private ResourceCost barrierCost;
    [SerializeField] private GameObject barrier;
    [SerializeField] private GameObject barrierUI;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject islandToSpawn;

    private bool isPlayerInRange = false;
    private bool isBarrierOpen = false;


    private void Start()
    {
        GameInputs.Instance.OnInteract += GameInputs_OnInteract;
        interactText.SetActive(false);
        islandToSpawn.SetActive(false);
    }
    private void OnDestroy()
    {
        
        if (GameInputs.Instance != null)
        {
            GameInputs.Instance.OnInteract -= GameInputs_OnInteract;
        }
    }
    private void GameInputs_OnInteract()
    {
        
        if (isPlayerInRange && !isBarrierOpen)
        {
            TryOpenBarrier();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !isBarrierOpen)
        {
            isPlayerInRange = true;
            interactText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(false);
            isPlayerInRange = false;
        }
    }

    private void TryOpenBarrier()
    {
        if (PlayerInventory.Instance.HasEnoughOres(barrierCost.ore, barrierCost.amount))
        {
            PlayerInventory.Instance.RemoveOres(barrierCost.ore, barrierCost.amount);
            OpenGate();
        }
    }
    private void OpenGate()
    {
        isBarrierOpen = true;
        barrier.SetActive(false);
        barrierUI.SetActive(false);
        interactText.SetActive(false);
        islandToSpawn.SetActive(true);
    }
}
