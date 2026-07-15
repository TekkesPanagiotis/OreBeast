using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private LaserEmitter laserEmitter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsRunning", playerMovement.IsRunning());
        animator.SetBool("IsGrappling", playerMovement.isGrappling);
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            animator.SetBool("Fire", laserEmitter.IsFiring());
        }
    }
}
