using UnityEngine;
using System;
public class GameInputs : MonoBehaviour
{
    public static GameInputs Instance { get; private set; }
    private PlayerInputActions playerInputActions;
    public event Action OnFireStarted;
    public event Action OnFireCanceled;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Fire.started += Firestarted;
        playerInputActions.Player.Fire.canceled += Firecanceled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    private void Firestarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnFireStarted?.Invoke();
    }
    private void Firecanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnFireCanceled?.Invoke(); // Shout that Fire was released
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Fire.started -= Firestarted;
        playerInputActions.Player.Fire.canceled -= Firecanceled;
        playerInputActions.Dispose();
    }

}
