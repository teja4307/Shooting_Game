using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class CustomOnScreenControl : OnScreenControl
{
    private string _controlPathInternal;
    private InputAction _inputAction;

    protected override string controlPathInternal
    {
        get => _controlPathInternal;
        set => _controlPathInternal = value;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("OnEnable called for OnScreenControl.");

        // Dynamically assign control paths
        AssignControlPaths();

        if (!string.IsNullOrEmpty(_controlPathInternal))
        {
            // Initialize and enable the InputAction
            _inputAction = new InputAction(binding: _controlPathInternal);
            _inputAction.Enable();
            _inputAction.performed += OnControlPerformed;
            _inputAction.canceled += OnControlCanceled;
            Debug.Log($"InputAction created with binding: {_controlPathInternal}");
        }
        else
        {
            Debug.LogError("Control path is empty or null.");
        }
    }

    private void AssignControlPaths()
    {
        // Example: Assign the control path to the first available gamepad button
        var gamepad = Gamepad.all.FirstOrDefault();
        if (gamepad != null)
        {
            _controlPathInternal = gamepad.rightTrigger.path;
            Debug.Log($"Assigned control path: {_controlPathInternal}");
        }
        else
        {
            //Debug.LogWarning("No gamepad found to assign control paths.");
            // Fall back to another device, for example, mouse left button
            var mouse = Mouse.current;
            if (mouse != null)
            {
                _controlPathInternal = mouse.leftButton.path;
                Debug.Log($"Assigned control path to mouse left button: {_controlPathInternal}");
            }
            else
            {
                Debug.LogError("No gamepad or mouse found to assign control paths.");
                _controlPathInternal = null;
            }
        }
    }

    private void OnControlPerformed(InputAction.CallbackContext context)
    {
        // Handle control performed
        Debug.Log($"Control performed: {context.control.name} with value {context.ReadValue<float>()}");
    }

    private void OnControlCanceled(InputAction.CallbackContext context)
    {
        // Handle control canceled
        Debug.Log($"Control canceled: {context.control.name}");
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Debug.Log("OnDisable called for OnScreenControl.");

        if (_inputAction != null)
        {
            _inputAction.Disable();
            _inputAction.performed -= OnControlPerformed;
            _inputAction.canceled -= OnControlCanceled;
        }
    }


    
}
