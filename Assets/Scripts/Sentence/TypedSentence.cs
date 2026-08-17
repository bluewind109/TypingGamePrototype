using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class TypedSentence
{
    private string _sentence = "";
    private ActiveSentenceDisplay _sentenceDisplay;
	private const int MAX_SENTENCE_LENGTH = 50;
	private char _lastTypedLetter = '\0';

    public TypedSentence(ActiveSentenceDisplay sentenceDisplay)
    {
        _sentenceDisplay = sentenceDisplay;
        UpdateText("");
    }

    public void Clear()
    {
        _lastTypedLetter = '\0';
        UpdateText("");
    }

    public List<string> GetWords()
    {
        List<string> words = new List<string>(_sentence.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        return words;
    }

    public void AddLetter(char letter)
    {
        if (_sentence.Length >= MAX_SENTENCE_LENGTH) return;
		if (letter == _lastTypedLetter && _lastTypedLetter == ' ') return; // Prevent multiple spaces in a row
        string newText = _sentence + letter;
        _lastTypedLetter = letter;
        UpdateText(newText);
    }

    public void RemoveLastLetter()
    {
        if (_sentence.Length == 0) return;
        string newText = _sentence.Substring(0, _sentence.Length - 1);
        _lastTypedLetter = _sentence.Length > 0 ? _sentence[_sentence.Length - 1] : '\0';
        UpdateText(newText);
    }

    private void UpdateText(string newText)
    {
        // Debug.Log($"Typed sentence updated: <color=green>{newText}</color>");
        _sentence = newText;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (_sentenceDisplay == null) return;
        _sentenceDisplay.SetSentence(_sentence);
    }
}
