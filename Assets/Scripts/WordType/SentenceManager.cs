using System;
using System.Collections.Generic;
using UnityEngine;

public class SentenceManager : MonoBehaviour
{
    public System.Action<CombatAction> onActionTyped;

    [Header("Components")]
    [SerializeField] private Transform _sentenceContainer;
    [SerializeField] private SentenceDisplay _sentenceDisplayPrefab;
    [SerializeField] private SentenceDisplay _activeSentenceDisplay;
    [SerializeField] private WordInput _wordInput;

    private List<Sentence> _sentences = new List<Sentence>();
    private List<CombatAction> _actions;
    private CombatAction _action;
    private Sentence _activeSentence;

    void Start()
    {
        _wordInput.onLetterTyped += TypeLetter;
    }

    public void Initialize(List<CombatAction> actions)
    {
        _actions = actions;
        foreach (CombatAction action in _actions)
        {
            Sentence sentence = new Sentence(
                action.actionName,
                Instantiate(_sentenceDisplayPrefab, _sentenceContainer)
            );

            _sentences.Add(sentence);
        }

        _activeSentence = new Sentence("", _activeSentenceDisplay);
    }

    public void ToggleInput(bool enabled)
    {
        _wordInput.ToggleInput(enabled);
    }

    public void UpdateGameplay()
    {
        _wordInput.UpdateInput();
    }

    private void TypeLetter(char letter)
    {
        foreach (Sentence sentence in _sentences)
        {
            if (sentence.GetNextLetter() == letter)
            {
                _activeSentence = sentence;
                sentence.TypeLetter();
                break;
            }
        }
    }
}

[Serializable]
public class Sentence
{
    public string sentence;
    private int typeIndex;
    private SentenceDisplay _sentenceDisplay;

    public Sentence(string sentence, SentenceDisplay sentenceDisplay)
    {
        this.sentence = sentence;
        typeIndex = 0;
        _sentenceDisplay = sentenceDisplay;
        _sentenceDisplay.SetSentence(sentence);
    }

    public char GetNextLetter()
    {
        return sentence[typeIndex];
    }

    public void TypeLetter()
    {
        typeIndex++;
    }

    public bool IsSentenceTyped()
    {
        bool sentenceTyped = typeIndex >= sentence.Length;
        return sentenceTyped;
    }
}

[Serializable]
public class TypedSentence
{
    public string sentence;
    private int typeIndex;
    
}
