using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class InputSystemBindingFixer : MonoBehaviour
{
    [Tooltip("The Player Input component managing your input actions.")]
    public PlayerInput playerInput;

    [Tooltip("Assign all OnScreenButton components here.")]
    public OnScreenButton[] onScreenButtons;

    private void Awake()
    {
        if (playerInput == null)
        {
            playerInput = FindObjectOfType<PlayerInput>();
        }

        ValidateBindings();
    }

    private void ValidateBindings()
    {
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component is missing. Please assign it in the inspector.");
            return;
        }

        if (onScreenButtons == null || onScreenButtons.Length == 0)
        {
            Debug.LogWarning("No OnScreenButtons assigned. Please add them to the array.");
            return;
        }

        foreach (var button in onScreenButtons)
        {
            if (button == null)
            {
                Debug.LogWarning("An OnScreenButton is null. Check the references.");
                continue;
            }

            var controlPath = button.controlPath;
            if (string.IsNullOrEmpty(controlPath))
            {
                Debug.LogError($"Control path for {button.name} is not set. Please assign it.");
                continue;
            }

            // Validate control path
            if (!ValidateControlPath(controlPath))
            {
                Debug.LogError($"Invalid control path '{controlPath}' for button: {button.name}");
            }
        }
    }

    private bool ValidateControlPath(string controlPath)
    {
        // Check if the control path matches a valid device binding
        var devices = InputSystem.devices;
        foreach (var device in devices)
        {
            var control = device.TryGetChildControl(controlPath);
            if (control != null)
            {
                Debug.Log($"Validated control path '{controlPath}' on device: {device.name}");
                return true;
            }
        }

       // Debug.LogError($"Control path '{controlPath}' does not match any available device.");
        return false;
    }
}
