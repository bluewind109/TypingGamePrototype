using System.Collections.Generic;
using UnityEngine;

public enum SentenceState
{
    Active = 0,
    Pending = 1,
    Finished = 2,
}

public class SentenceManager : MonoBehaviour
{
    public System.Action<CombatAction> onActionTyped;

    [Header("Components")]
    [SerializeField] private Transform _sentenceContainer;
    [SerializeField] private SentenceDisplay _sentenceDisplayPrefab;
    [SerializeField] private WordInput _wordInput;

    private List<SentenceDisplay> _sentenceDisplays = new List<SentenceDisplay>();
    private List<Sentence> _sentences = new List<Sentence>();
    private List<CombatAction> _actions;
    private CombatAction _action;
    private Sentence _activeSentence;

    public bool HasActiveSentence => _activeSentence != null;

    void Start()
    {
        _wordInput.onLetterTyped += TypeLetter;
    }

    public void Initialize(List<CombatAction> actions)
    {
        _actions = actions;
        foreach (CombatAction action in _actions)
        {
            SentenceDisplay sentenceDisplayInstance = Instantiate(_sentenceDisplayPrefab, _sentenceContainer);
            _sentenceDisplays.Add(sentenceDisplayInstance);
        }
    }

    public void Reset()
    {
        _sentences.Clear();
        _activeSentence = null;
        _wordInput.ToggleInput(false);
    }

    public void ToggleInput(bool enabled)
    {
        _wordInput.ToggleInput(enabled);
    }

    private void TypeLetter(char letter)
    {
        if (HasActiveSentence)
        {
            // Check if letter was next
            if (_activeSentence.GetNextLetter() == letter)
            {
                _activeSentence.TypeLetter();
            }
        }
        else
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

        if (HasActiveSentence && _activeSentence.SentenceTyped())
        {
            // onActionTyped?.Invoke(_action);
        }
    }
}

[System.Serializable]
public class Sentence
{
    public string sentence;
    private int typeIndex;
    private SentenceDisplay sentenceDisplay;

    public Sentence(string sentence, SentenceDisplay sentenceDisplay)
    {
        this.sentence = sentence;
        typeIndex = 0;
        this.sentenceDisplay = sentenceDisplay;
        this.sentenceDisplay.SetSentence(sentence, SentenceState.Active);
    }

    public char GetNextLetter()
    {
        return sentence[typeIndex];
    }

    public void TypeLetter()
    {
        typeIndex++;
        sentenceDisplay.RemoveLetter();
    }

    public bool SentenceTyped()
    {
        bool sentenceTyped = typeIndex >= sentence.Length;
        if (sentenceTyped)
        {
            sentenceDisplay.RemoveSentence();
        }
        return sentenceTyped;
    }
}
