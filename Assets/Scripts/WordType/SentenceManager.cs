using System;
using System.Collections.Generic;
using UnityEngine;

public class SentenceManager : MonoBehaviour
{
    public System.Action<CombatAction> onActionTyped;

    [Header("Components")]
    [SerializeField] private Transform _sentenceContainer;
    [SerializeField] private SentenceDisplay _sentenceDisplayPrefab;
    [SerializeField] private SentenceDisplay _typedSentenceDisplay;
    [SerializeField] private WordInput _wordInput;

    private List<Sentence> _sentences = new List<Sentence>();
    private List<CombatAction> _availableActions;
    private TypedSentence _typedSentence;

    void Start()
    {
        _wordInput.onLetterTyped += OnLetterTyped;
        _wordInput.onBackspaceTyped += OnBackspaceTyped;
        _wordInput.OnEnterTyped += OnEnterTyped;
    }

    public void Initialize(List<CombatAction> availableActions)
    {
        _availableActions = availableActions;
        foreach (CombatAction action in _availableActions)
        {
            Sentence sentence = new Sentence(
                action.actionName,
                Instantiate(_sentenceDisplayPrefab, _sentenceContainer)
            );

            _sentences.Add(sentence);
        }

        _typedSentence = new TypedSentence(_typedSentenceDisplay);
    }

    public void ToggleInput(bool enabled)
    {
        _wordInput.ToggleInput(enabled);
    }

    public void UpdateGameplay()
    {
        _wordInput.UpdateInput();
    }

    public void ResetTypedSentence()
    {
        _typedSentence = new TypedSentence(_typedSentenceDisplay);
    }

    private void OnLetterTyped(char letter)
    {
        _typedSentence.AddLetter(letter);
    }

    private void OnBackspaceTyped()
    {
        _typedSentence.RemoveLastLetter();
    }

    private void OnEnterTyped()
    {
        // TODO - Check if typed sentence matches any available action
    }
}

