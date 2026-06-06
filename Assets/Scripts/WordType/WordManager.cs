using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager Instance;

    [SerializeField] private WordSpawner wordSpawner;
    [SerializeField] private WordInput wordInput;
    [SerializeField] private WordTimer wordTimer;

    List<Word> words = new List<Word>();

    private bool hasActiveWord = false;
    private Word activeWord;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        WordGenerator.LoadWords();
        wordInput.onLetterTyped += TypeLetter;
        wordTimer.onWordTimeout += AddWord;
    }

    void OnDestroy()
    {
        Instance = null;
        wordInput.onLetterTyped -= TypeLetter;
        wordTimer.onWordTimeout -= AddWord;
    }

    public void AddWord()
    {
        Word word = new Word(WordGenerator.GetRandomWord(), wordSpawner.SpawnWord());
        words.Add(word);
        // Debug.Log("Added word: " + word.word);
    }

    private void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {
            // Check if letter was next
            if (activeWord.GetNextLetter() == letter)
            {
                activeWord.TypeLetter();
            }
        }
        else
        {
            foreach (Word word in words)
            {
                if (word.GetNextLetter() == letter)
                {
                    activeWord = word;
                    hasActiveWord = true;
                    word.TypeLetter();
                    break;
                }
            }
        }

        if (hasActiveWord && activeWord.WordTyped())
        {
            hasActiveWord = false;
            words.Remove(activeWord);
        }
    }
}

[System.Serializable]
public class Word
{
    public string word;
    private int typeIndex;
    private WordDisplay wordDisplay;

    public Word(string _word, WordDisplay _wordDisplay)
    {
        word = _word;
        typeIndex = 0;
        wordDisplay = _wordDisplay;
        wordDisplay.SetWord(word);
    }

    public char GetNextLetter()
    {
        return word[typeIndex];
    }

    public void TypeLetter()
    {
        typeIndex++;
        wordDisplay.RemoveLetter();
    }

    public bool WordTyped()
    {
        bool wordTyped = typeIndex >= word.Length;
        if (wordTyped)
        {
            wordDisplay.RemoveWord();
        }
        return wordTyped;
    }
}
