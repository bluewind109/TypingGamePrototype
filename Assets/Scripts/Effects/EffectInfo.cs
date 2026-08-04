using System;
using UnityEngine;

[Serializable]
public class EffectInfo
{
    public EffectType type;
    public int potency; // e.g., amount of damage or healing
    public TargetType targetType;
    public TargetTeam targetTeam;

    public void IncreasePotency()
    {
        potency *= 2;
    }

    public void DecreasePotency()
    {
        potency = Mathf.Max(1, potency / 2);
    }
}

public enum EffectType
{
    Damage = 0,
    Heal = 1,
    Shield = 2,
}
