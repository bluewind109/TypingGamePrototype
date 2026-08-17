using UnityEngine;
using System;

public class GameDefines
{

}

public enum SkillTag
{
    Basic = 0,
    Attack = 1,
    Defend = 2,
    Advanced = 3,
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

public enum StatusEffectType
{
    Parry = 0,
    Burn = 1,
    Poison = 2,
}