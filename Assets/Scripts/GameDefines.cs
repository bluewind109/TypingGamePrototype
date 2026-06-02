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
