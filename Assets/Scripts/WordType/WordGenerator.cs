using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    private static string[] wordList;

    private static Dictionary<EffectType, string> effectWordMap = new Dictionary<EffectType, string>()
    {
        { EffectType.Damage, "Attack" },
        { EffectType.Heal, "Heal" },
        { EffectType.Shield, "Shield" },
    };

    private static Dictionary<TargetTeam, string> targetWordMap = new Dictionary<TargetTeam, string>()
    {
        { TargetTeam.Enemy, "the enemy" },
        { TargetTeam.Ally, "your ally" },
        { TargetTeam.Self, "yourself" },
    };

    public static void LoadWords()
    {
        wordList = File.ReadAllLines(Application.streamingAssetsPath + "/wordList.txt");
    }

    public static string GetRandomWord()
    {
        int randomIndex = Random.Range(0, wordList.Length);
        return wordList[randomIndex];
    }

    public static string GetWordForEffect(EffectType effectType)
    {
        if (effectWordMap.TryGetValue(effectType, out string word))
        {
            return word;
        }

        return "";
    }

    public static string GetWordForTarget(TargetTeam targetTeam)
    {
        if (targetWordMap.TryGetValue(targetTeam, out string word))
        {
            return word;
        }

        return "";
    }
}
