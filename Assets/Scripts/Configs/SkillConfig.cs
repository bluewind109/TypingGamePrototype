using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillConfig", menuName = "Configs")]
public class SkillConfig : ScriptableObject
{
    public List<Skill> basicSkills = new List<Skill>();
    public List<Skill> advancedSkills = new List<Skill>();

    public List<Skill> GetAllSkills()
    {
        List<Skill> allSkills = new List<Skill>();
        allSkills.AddRange(basicSkills);
        allSkills.AddRange(advancedSkills);
        return allSkills;
    }
}
