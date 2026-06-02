using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionConfig", menuName = "Configs")]
public class ActionConfig : ScriptableObject
{
    public CombatAction basicAttack;
    public CombatAction basicDefend;
    public List<CombatAction> skills = new List<CombatAction>();
}
