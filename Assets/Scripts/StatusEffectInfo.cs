using UnityEngine;

[CreateAssetMenu(fileName = "NewStatusEffectInfo", menuName = "Status Effect Info")]
public class StatusEffectInfo : ScriptableObject
{
    public string effectName;
    public StatusEffectType effectType;
    public int maxStacks = 1;
    public float duration;
}

