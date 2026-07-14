using UnityEngine;
using System;
public class GameInputs : MonoBehaviour
{
    public static GameInputs Instance { get; private set; }
    private PlayerInputActions playerInputActions;
    public event Action OnFireStarted;
    public event Action OnFireCanceled;
    //Inventory EVENTS
    public event Action OnInventoryToggle;
    //UPGRADE EVENTS
    public event Action OnUpgradeMenuToggle;
    //INTERACT EVENTS
    public event Action OnInteract;

    //JETPACK EVENTS
    public event Action OnJumpStarted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        //LASER GUN EVENTS
        playerInputActions.Player.Fire.started += Firestarted;
        playerInputActions.Player.Fire.canceled += Firecanceled;

        //INVENTORY EVENTS
        playerInputActions.Player.Inventory.performed += ToggleInventory;

        //UPGRADE EVENTS
        playerInputActions.Player.Upgrade.performed += ToggleUpgradeMenu;

        //INTERACT EVENTS
        playerInputActions.Player.Interact.performed += TryInteract;
        //JETPACK EVENTS
        playerInputActions.Player.Fly.started += OnJumpStart;
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
        OnFireCanceled?.Invoke();
    }

    private void ToggleInventory(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInventoryToggle?.Invoke();
    }

    private void ToggleUpgradeMenu(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnUpgradeMenuToggle?.Invoke();
    }

    private void TryInteract(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

    private void OnJumpStart(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpStarted?.Invoke();
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Fire.started -= Firestarted;
        playerInputActions.Player.Fire.canceled -= Firecanceled;
        playerInputActions.Player.Inventory.performed -= ToggleInventory;
        playerInputActions.Player.Upgrade.performed -= ToggleUpgradeMenu;
        playerInputActions.Player.Interact.performed -= TryInteract;
        playerInputActions.Player.Fly.started -= OnJumpStart;
        playerInputActions.Dispose();
    }

}
