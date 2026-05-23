using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack", menuName = "Actions/Basic/Basic Attack")]
public class Action_BasicAttack : Action_Attack
{
    protected override void ConfigureTags()
    {
        base.ConfigureTags();
        EnsureTag(ActionTag.Basic);
    }

    public override void Execute(GameObject target)
    {
        // TODO deal damage to the enemy equal to attackDamage
        // Give the player 1 AP
    }
}
