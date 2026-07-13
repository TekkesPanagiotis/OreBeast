using UnityEngine;

public class LaserGunAudio : MonoBehaviour
{

    [SerializeField] private AudioClip laserSound;
    private AudioSource audioSource;
    private LaserController laserController;
  
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        laserController = GetComponent<LaserController>();
        audioSource.clip = laserSound;
    }

    private void OnEnable()
    {
        laserController.OnTriggerHold += laserController_OnTriggerHold;
        laserController.OnTriggerReleased += laserController_OnTriggerReleased;
    }

    private void OnDisable()
    {
        laserController.OnTriggerHold -= laserController_OnTriggerHold;
        laserController.OnTriggerReleased -= laserController_OnTriggerReleased;
    }

    private void laserController_OnTriggerHold()
    {
        audioSource.pitch = Random.Range(1.2f, 1.4f);
        audioSource.Play();
    }

    private void laserController_OnTriggerReleased()
    {
        audioSource.Stop();
    }
}
