using UnityEngine;

public abstract class Action_Defend : CombatAction
{
    protected override void ConfigureTags()
    {
        base.ConfigureTags();
        EnsureTag(ActionTag.Defend);
    }
}
