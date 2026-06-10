using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    public void HandleAction(CombatAction action, Entity source, Entity primaryEnemyTarget)
    {
        if (action == null || source == null)
        {
            Debug.LogWarning("EffectHandler.HandleAction called with null action or source.");
            return;
        }

        foreach (EffectInfo effectInfo in action.effects)
        {
            Entity resolvedTarget = ResolveTarget(effectInfo.targetTeam, source, primaryEnemyTarget);
            if (resolvedTarget == null)
            {
                Debug.LogWarning($"No valid target resolved for effect {effectInfo.effect?.effectName}.");
                continue;
            }

            HandleEffect(effectInfo.effect, resolvedTarget, effectInfo.potency);
        }
    }

    private static Entity ResolveTarget(TargetTeam targetTeam, Entity source, Entity primaryEnemyTarget)
    {
        switch (targetTeam)
        {
            case TargetTeam.Self:
                return source;
            case TargetTeam.Enemy:
                return primaryEnemyTarget;
            case TargetTeam.Ally:
                // Until ally party members exist, treat ally as self.
                return source;
            default:
                return null;
        }
    }

    private static void HandleEffect(Effect effect, Entity target, int potency)
    {
        if (effect == null)
        {
            return;
        }

        effect.ApplyEffect(target, potency);
    }
}
