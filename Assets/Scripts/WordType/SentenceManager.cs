using System.Collections.Generic;
using UnityEngine;

public class SentenceManager : MonoBehaviour
{
    public System.Action<CombatAction> onActionTyped;

    [Header("Components")]
    [SerializeField] private SentenceDisplay sentenceDisplay;
    [SerializeField] private WordInput wordInput;
    [SerializeField] private WordTimer wordTimer;
    
    [Header("Test")]
    [SerializeField] private CombatAction testAction;

    List<Sentence> sentences = new List<Sentence>();

    public bool HasActiveSentence => activeSentence != null;

    private CombatAction action;
    private List<EffectInfo> effects;
    private Sentence activeSentence;

    private int effectIndex = 0;

    void Start()
    {
        wordInput.onLetterTyped += TypeLetter;
        wordTimer.onWordTimeout += OnSentenceTimedout;
    }

    public void TestLoadAction()
    {
        // Create a test action here
        LoadAction(testAction);
    }

    public void LoadAction(CombatAction action)
    {
        this.action = action;
        effects = action.effects;
        foreach ( var effect in action.effects)
        {
            AddSentence(effect);
        }

        effectIndex = 0;
        SetActiveSentence(sentences[0]);
    }

    public void AddSentence(EffectInfo effectInfo)
    {
        var effectWord = WordGenerator.GetWordForEffect(effectInfo.effect.effectType);
        var targetWord = WordGenerator.GetWordForTarget(effectInfo.targetTeam);

        var resultSentence = effectWord + " " + targetWord;

        Sentence sentence = new Sentence(resultSentence, sentenceDisplay);
        sentences.Add(sentence);
    }

    private void SetActiveSentence(Sentence sentence)
    {
        activeSentence = sentence;
        sentenceDisplay.SetSentence(sentence.sentence);
        wordTimer.StartTimer();
    }

    private Sentence GetNextSentence()
    {
        if (sentences.Count > 0)
        {
            return sentences[0];
        }
        return null;
    }

    private void TypeLetter(char letter)
    {
        if (HasActiveSentence)
        {
            // Check if letter was next
            if (activeSentence.GetNextLetter() == letter)
            {
                activeSentence.TypeLetter();
            }
        }
        else
        {
            foreach (Sentence sentence in sentences)
            {
                if (sentence.GetNextLetter() == letter)
                {
                    activeSentence = sentence;
                    sentence.TypeLetter();
                    break;
                }
            }
        }

        if (HasActiveSentence && activeSentence.SentenceTyped())
        {
            sentences.Remove(activeSentence);
            effectIndex++;
            var nextSentence = GetNextSentence();
            if (nextSentence == null)
            {
                activeSentence = null;
                onActionTyped?.Invoke(action);
            }
            else
            {
                SetActiveSentence(nextSentence);
            }
        }
    }

    private void OnSentenceTimedout()
    {
        action.DecreaseEffectPotency(effectIndex);
    }
}

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
        this.sentenceDisplay.SetSentence(sentence);
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
