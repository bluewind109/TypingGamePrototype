using UnityEngine;

[CreateAssetMenu(fileName = "BasicDefend", menuName = "Actions/Basic/Basic Defend")]
public class Action_BasicDefend : Action_Defend
{
    protected override void ConfigureTags()
    {
        base.ConfigureTags();
        EnsureTag(ActionTag.Basic);
    }

    public override void Execute()
    {
        // TODO Apply 1 defence stack to the player
        // Give the player 1 AP when consume 1 stack of defence
    }
}
