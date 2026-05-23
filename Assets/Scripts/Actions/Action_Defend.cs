using UnityEngine;

public abstract class Action_Defend : Action
{
    protected override void ConfigureTags()
    {
        base.ConfigureTags();
        EnsureTag(ActionTag.Defend);
    }
}
