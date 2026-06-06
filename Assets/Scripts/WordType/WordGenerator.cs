using System.IO;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    private static string[] wordList;

    public static void LoadWords()
    {
        wordList = File.ReadAllLines(Application.streamingAssetsPath + "/wordList.txt");
    }

    public static string GetRandomWord()
    {
        int randomIndex = Random.Range(0, wordList.Length);
        return wordList[randomIndex];
    }
}
