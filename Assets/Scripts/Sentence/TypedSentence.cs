using UnityEngine;
using System;

[Serializable]
public class TypedSentence
{
    private string _sentence = "";
    private SentenceDisplay _sentenceDisplay;

    public TypedSentence(SentenceDisplay sentenceDisplay)
    {
        _sentenceDisplay = sentenceDisplay;
        UpdateText("");
    }

    public void AddLetter(char letter)
    {
        string newText = _sentence + letter;
        UpdateText(newText);
    }

    public void RemoveLastLetter()
    {
        if (_sentence.Length == 0) return;
        string newText = _sentence.Substring(0, _sentence.Length - 1);
        UpdateText(newText);
    }

    private void UpdateText(string newText)
    {
        Debug.Log($"Typed sentence updated: <color=green>{newText}</color>");
        _sentence = newText;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (_sentenceDisplay == null) return;
        _sentenceDisplay.SetSentence(_sentence);
    }
}
