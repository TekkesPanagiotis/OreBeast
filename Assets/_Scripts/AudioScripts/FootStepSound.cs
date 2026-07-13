using System.Collections;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AudioSource audioSource;
    [SerializeField] private AudioClip stepSound;

    [SerializeField] private float strideLength = 2.0f;
    private Vector3 lastPosition;
    private float distanceTraveled;

    private float minPitch = 0.9f;
    private float maxPitch = 1.2f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        lastPosition = transform.position;
    }
    private void Update()
    {
        
        float currentFrameDistance = Vector3.Distance(transform.position, lastPosition);

        if (playerMovement.IsRunning())
        {
            
            distanceTraveled += currentFrameDistance;

            
            if (distanceTraveled >= strideLength)
            {
                PlayFootStep();

                
                distanceTraveled %= strideLength;
            }
        }
        else
        {
            
            distanceTraveled = strideLength * 0.8f;
        }

        lastPosition = transform.position;
    }

    private void PlayFootStep()
    {
    
        if (stepSound == null) return;

        audioSource.pitch = Random.Range(minPitch, maxPitch);

        audioSource.PlayOneShot(stepSound);
    }
}
