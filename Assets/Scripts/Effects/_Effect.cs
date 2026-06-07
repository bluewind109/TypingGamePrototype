using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public string effectName;
    public EffectType effectType;

    public abstract void ApplyEffect();
}
