using UnityEngine;
using System;

public class GameDefines
{

}

/// <summary>
/// 1/ Turn Start: 
/// - Show Enemy incoming actions
/// - Player selects action to put into queue (max action is 6 by default)
/// - When done selecting, player can start combat phase
/// 2/ Combat:
/// - Player has to type the action to execute it
/// - Enemy executes its actions in order in intervals (e.g., every 2 seconds)
/// 3/ Turn End:
/// - Resolve any status effects, cooldowns, and other end-of-turn mechanics
/// - If enemy is still alive, start a new turn
/// </summary>
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

public enum ActionButtonType
{
    BasicAttack = 0,
    BasicDefend = 1,
    Skill = 2,
}

public enum StatusEffectType
{
    Parry = 0,
    Burn = 1,
    Poison = 2,
}