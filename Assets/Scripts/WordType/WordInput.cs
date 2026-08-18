using UnityEngine;
using System;

public class WordInput : MonoBehaviour
{
    public Action<char> LetterTyped;
    public Action BackspaceTyped;
    public Action EnterTyped;

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
        if (!Input.anyKeyDown) return;

        foreach (char letter in Input.inputString)
        {
            if (!IsAllowedCharacter(letter)) continue;
            if (letter == '\b')
            {
                BackspaceTyped?.Invoke();
            }
            else if (letter == '\n' || letter == '\r')
            {
                EnterTyped?.Invoke();
            }
            else
            {
                LetterTyped?.Invoke(letter);
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
