using UnityEngine;

public abstract class Action_Attack : CombatAction
{
    [Min(0)] public int attackDamage = 1;

    protected override void ConfigureTags()
    {
        base.ConfigureTags();
        EnsureTag(ActionTag.Attack);
    }
}
