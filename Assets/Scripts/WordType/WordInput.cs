using UnityEngine;
using System;

public class WordInput : MonoBehaviour
{
    public Action<char> onLetterTyped;
    public Action onBackspaceTyped;
    public Action OnEnterTyped;

    private bool isEnabled = false;

    public void ToggleInput(bool enabled)
    {
        string status = enabled ? "<color=green>enabled</color>" : "<color=red>disabled</color>";
        Debug.Log($"Input status: {status}");
        isEnabled = enabled;
    }

    public void UpdateInput()
    {
        if (!isEnabled) return;

        foreach (char letter in Input.inputString)
        {
            if (!IsAllowedCharacter(letter)) continue;
            if (letter == '\b')
            {
                onBackspaceTyped?.Invoke();
            }
            else if (letter == '\n' || letter == '\r')
            {
                OnEnterTyped?.Invoke();
            }
            else
            {
                onLetterTyped?.Invoke(letter);
                // Debug.Log("Typed letter: " + letter);
            }
        }
    }

    /// <summary>
    /// Only alphabetic characters, space and - are allowed
    /// Backspace and enter are also allowed for input handling
    /// </summary>
    private bool IsAllowedCharacter(char _letter)
    {
        // Debug.Log("Allowed character: " + _letter);
        bool normalLetter = char.IsLetter(_letter);
        bool spaceOrDash = _letter == ' ' || _letter == '-';
        bool backspaceTyped = _letter == '\b';
        bool enterTyped = _letter == '\n' || _letter == '\r';
        return normalLetter || spaceOrDash || backspaceTyped || enterTyped;
    }

}
