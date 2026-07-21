using UnityEngine;

public class RestartChallenge : MonoBehaviour
{
    [SerializeField] private Transform darkIslandChallengeStart;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = darkIslandChallengeStart.position;
        }
    }
}
