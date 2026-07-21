using System;
using UnityEngine;

public class BaseIslandTeleporter : MonoBehaviour
{
    [SerializeField] private Transform baseIslandPosition;
    [SerializeField] private OreDataSO sapphire;
    [SerializeField] private GameObject lights;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             other.gameObject.transform.position = baseIslandPosition.position;
            lights.SetActive(true);
            PlayerInventory.Instance.AddItem(sapphire, 200);
        }
    }
}
