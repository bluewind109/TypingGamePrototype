using UnityEngine;
using System;

public class GameDefines
{

}

[Serializable]
public class EffectInfo
{
    public Effect effect;
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

public enum ActionTag
{
    Basic = 0,
    Attack = 1,
    Defend = 2,
    Skill = 3,
}

public enum TargetType
{
    Single = 0,
    Multiple = 1,
}

public enum TargetTeam
{
    Enemy = 0,
    Ally = 1,
    Self = 2,
}

public enum EffectType
{
    Damage = 0,
    Heal = 1,
    Shield = 2,
}

public enum ActionButtonType
{
    BasicAttack = 0,
    BasicDefend = 1,
    Skill = 2,
}

public enum SentenceState
{
    Active = 0,
    Pending = 1,
    Finished = 2,
}