using UnityEngine;

public class WordInput : MonoBehaviour
{
    public System.Action<char> onLetterTyped;

    void Update()
    {
        foreach (char letter in Input.inputString)
        {
            if (!IsAllowedCharacter(letter)) continue;
            onLetterTyped?.Invoke(letter);
            Debug.Log("Typed letter: " + letter);
        }
    }

    /// <summary>
    /// Only alphabetic characters, space and - are allowed
    /// </summary>
    /// <param name="_letter"></param>
    /// <returns></returns>
    private bool IsAllowedCharacter(char _letter)
    {
        Debug.Log("Allowed character: " + _letter);
        return char.IsLetter(_letter) || _letter == ' ' || _letter == '-';
    }

}
