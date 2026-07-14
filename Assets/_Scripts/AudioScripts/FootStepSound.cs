using System.Collections;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] stepSounds;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //WILL PLAY THE FUNCTION WITH ANIMATION EVENT ON WALKING
    private void PlayRandomFootStepSound()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length)]);
    }
}
