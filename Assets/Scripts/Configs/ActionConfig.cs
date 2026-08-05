using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionConfig", menuName = "Configs")]
public class ActionConfig : ScriptableObject
{
    public List<CombatAction> basicSkills = new List<CombatAction>();
    public List<CombatAction> advancedSkills = new List<CombatAction>();
}
