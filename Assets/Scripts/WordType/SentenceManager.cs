using System;
using System.Collections.Generic;
using UnityEngine;

public class SentenceManager : MonoBehaviour
{
    public Action<List<CombatAction>> onActionsTyped;

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
        _wordInput.LetterTyped += OnLetterTyped;
        _wordInput.BackspaceTyped += OnBackspaceTyped;
        _wordInput.EnterTyped += OnEnterTyped;
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
        _typedSentence.Clear();
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
        // Check if typed sentence matches any available action
        CheckTypedSentence();

    }

    /// <summary>
    /// - Split the sentence into words and check each word against the available actions.
    /// - Checks if the typed sentence matches any of the available actions.
    /// - If matched, remove the matched word from the typed sentence and add to the found actions list.
    /// - Continue to check the remaining typed sentence for more matches until no more matches are found;.
    /// - Pass the found actions list to the onActionsTyped event for further processing.
    /// - Clear the typed sentence after processing.
    /// </summary>
    private void CheckTypedSentence()
    {
        // Split the sentence into words and check each word against the available actions.
        List<string> typedWords = _typedSentence.GetWords();
        Debug.Log($"Typed words: <color=green>{string.Join(", ", typedWords)}</color>");

        // Checks if the typed sentence matches any of the available actions.
        List<CombatAction> foundActions = new List<CombatAction>();
        foreach (string typedWord in typedWords)
        {
            CombatAction matchedAction = IsWordMatchedAction(typedWord);
            if (matchedAction == null) continue;

            // TODO If matched, remove the matched word from the displayed typed sentence
            // _typedSentence.RemoveWord(typedWord);

            // Add to the found actions list.
            foundActions.Add(matchedAction);
        }
        // Pass the found actions list to the onActionsTyped event for further processing.
        if (foundActions.Count > 0)
        {
            onActionsTyped?.Invoke(foundActions);
        }
        // Clear the typed sentence after processing.
        ResetTypedSentence();
    }

    private CombatAction IsWordMatchedAction(string typedWord)
    {
        foreach (CombatAction action in _availableActions)
        {
            if (string.Equals(typedWord, action.actionName, StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log($"Found matching action: <color=green>{action.actionName}</color>");
                return action;
            }
        }

        Debug.Log($"No matching action found for typed word: <color=red>{typedWord}</color>");
        return null;
    }
}

