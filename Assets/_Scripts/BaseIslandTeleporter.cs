using UnityEngine;

public class BaseIslandTeleporter : MonoBehaviour
{
    [SerializeField] private Transform baseIslandPosition;
   

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
              other.gameObject.transform.position = baseIslandPosition.position;
        }
    }
}
