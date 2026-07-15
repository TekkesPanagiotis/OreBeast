using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private GameInputs gameInputs;
    [SerializeField] private Transform cameraTransform;
    private bool isRunning;
   
    public bool isGrappling = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       
    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        if (isGrappling) return;


        Vector2 inputVector = gameInputs.GetMovementVectorNormalized();
        //Get Camera rotation
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();
        //Move based on the camera direction
        Vector3 moveDir = (camForward * inputVector.y) + (camRight * inputVector.x);
        //CHECK IF PLAYER CAN MOVE
        float playerRadius = 0.5f;
        float PlayerHeight = 2f;
        //HORIZONTAL
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, playerRadius, moveDir, moveSpeed * Time.deltaTime);
        if (!canMove)
        {
            //Attempt move on X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, playerRadius, moveDirX, moveSpeed * Time.deltaTime);
            if (canMove)
            {
                //CAN MOVE ON THE X
                moveDir = moveDirX;
            }
            else
            {
                //ATTEMPT ONLY Z MOVEMENT
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, playerRadius, moveDirZ, moveSpeed * Time.deltaTime);
                if (canMove)
                {
                    //CAN MOVE ON THE Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //CANT MOVE ANYWHERE
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        
        isRunning = moveDir != Vector3.zero;
    }
    public bool IsRunning()
    {
        return isRunning;
    }
}
