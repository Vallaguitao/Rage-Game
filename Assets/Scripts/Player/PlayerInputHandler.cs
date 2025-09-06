using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerController;

    [Header("Input Action Map References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Input Action Name Reference")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string cameraChange = "ChangeCamera";
    [SerializeField] private string pause = "Pause";
    [SerializeField] private string powerUp = "PowerUp";
    [SerializeField] private string cancel = "Cancel";

    [Header("Deadzone Values")]
    [SerializeField] private float leftStickDeadzoneValue;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction interactAction;
    private InputAction cameraChangeAction;
    private InputAction pauseAction;
    private InputAction powerUpAction;
    private InputAction cancelAction;

    public Vector2 MoveInput { get; private set; }


    public InputAction jumpInput
    {
        get
        {
            return jumpAction;
        }
        set
        {
            jumpAction = value;
        }
    }


    public InputAction InteractInput
    {
        get
        {
            return interactAction;
        }
        set
        {
            interactAction = value;
        }
    }

    public InputAction CameraChangeInput
    {
        get
        {
            return cameraChangeAction;
        }
        set
        {
            cameraChangeAction = value;
        }
    }

    public InputAction PauseInput
    {
        get
        {
            return pauseAction;
        }
        set
        {
            pauseAction = value;
        }
    }

    public InputAction PowerUpInput
    {
        get
        {
            return powerUpAction;
        }
        set
        {
            powerUpAction = value;
        }
    }

    public InputAction cancelInput
    {
        get
        {
            return cancelAction;
        }
        set
        {
            cancelAction = value;
        }
    }

    private void Awake()
    {

        moveAction = playerController.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerController.FindActionMap(actionMapName).FindAction(jump);
        cameraChangeAction = playerController.FindActionMap(actionMapName).FindAction(cameraChange);
        interactAction = playerController.FindActionMap(actionMapName).FindAction(interact);
        pauseAction = playerController.FindActionMap(actionMapName).FindAction(pause);
        powerUpAction = playerController.FindActionMap(actionMapName).FindAction(powerUp);
        cancelAction = playerController.FindActionMap(actionMapName).FindAction(cancel);

        RegisterInputAction();

        InputSystem.settings.defaultDeadzoneMin = leftStickDeadzoneValue;

        PrintDevices();
    }

    private void RegisterInputAction()
    {

        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;


    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        interactAction.Enable();
        cameraChangeAction.Enable();
        pauseAction.Enable();
        powerUpAction.Enable();
        cancelAction.Enable();

        InputSystem.onDeviceChange += OnDeviceChange;
    }

    public void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        interactAction.Disable();
        cameraChangeAction.Disable();
        pauseAction.Disable();
        powerUpAction.Disable();
        cancelAction.Disable();

        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void PrintDevices()
    {
        foreach (var device in InputSystem.devices)
        {
            if (device.enabled)
            {
                Debug.Log($"Active Device: {device.name}");
            }
        }
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                Debug.Log($"Device is disconnected {device.name}");
                break;

            case InputDeviceChange.Reconnected:
                Debug.Log($"Device is reconnected {device.name}");
                break;
        }
    }
}
