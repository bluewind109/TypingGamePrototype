using UnityEngine;
using System;

[Serializable]
public class Sentence
{
    public string sentence;
    private SentenceDisplay _sentenceDisplay;

    public Sentence(string sentence, SentenceDisplay sentenceDisplay)
    {
        this.sentence = sentence;
        _sentenceDisplay = sentenceDisplay;
        _sentenceDisplay?.SetSentence(sentence);
    }
}