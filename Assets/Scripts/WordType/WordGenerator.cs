using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    private static string[] wordList;

    public static void LoadWords()
    {
        wordList = File.ReadAllLines(Application.streamingAssetsPath + "/wordList.txt");
    }
}
