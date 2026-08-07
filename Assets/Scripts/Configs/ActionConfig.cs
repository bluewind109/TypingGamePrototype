using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionConfig", menuName = "Configs")]
public class ActionConfig : ScriptableObject
{
    public List<CombatAction> basicSkills = new List<CombatAction>();
    public List<CombatAction> advancedSkills = new List<CombatAction>();

    public List<CombatAction> GetAllSkills()
    {
        List<CombatAction> allSkills = new List<CombatAction>();
        allSkills.AddRange(basicSkills);
        allSkills.AddRange(advancedSkills);
        return allSkills;
    }
}
