using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserController : MonoBehaviour
{
    public event Action OnTriggerHold;
    public event Action OnTriggerReleased;
   [SerializeField] private GameInputs gameInputs;

    private void Start()
    {
        GameInputs.Instance.OnFireStarted += OnLaserStarted;
        GameInputs.Instance.OnFireCanceled += OnLaserEnded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLaserStarted()
    {
        OnTriggerHold?.Invoke();
    }
    private void OnLaserEnded()
    {
        OnTriggerReleased?.Invoke();
    }
}
