using UnityEngine;

public abstract class Action_Skill : CombatAction
{
    public int actionCost;
    
    protected override void ConfigureTags()
    {
        base.ConfigureTags();
        EnsureTag(ActionTag.Skill);
    }
}