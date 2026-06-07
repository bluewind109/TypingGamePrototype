using UnityEngine;
using System;

public class ActionMenuInput : MonoBehaviour
{
    public Action onNavigateUp;
    public Action onNavigateDown;
    public Action onBack;
    public Action onEnter;

    private bool isEnabled = false;

    public void ToggleInput(bool enabled)
    {
        isEnabled = enabled;
    }

    void Update()
    {
        if (!isEnabled) return;
        if (!IsAllowedInput()) return;

        // Handle input for action menu navigation and selection
        // This is where you would implement the logic to navigate through the action menu
        // and select actions based on the player's input.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            onNavigateUp?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            onNavigateDown?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            onEnter?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            onBack?.Invoke();
        }
    }

    // Allow only up/down arrows, enter key and backspace for input
    private bool IsAllowedInput()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) ||
               Input.GetKeyDown(KeyCode.DownArrow) ||
               Input.GetKeyDown(KeyCode.Return) ||
               Input.GetKeyDown(KeyCode.Backspace);
    }
}
