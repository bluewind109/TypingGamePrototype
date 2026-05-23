using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionConfig", menuName = "Configs")]
public class ActionConfig : ScriptableObject
{
    public List<CombatAction> basicActions = new List<CombatAction>();
    public List<Action_Skill> skillActions = new List<Action_Skill>();
}
