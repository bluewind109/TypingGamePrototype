using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    private List<StatusEffect> activeStatusEffects = new List<StatusEffect>();

    private void Update()
    {
        TickStatusEffects(Time.deltaTime);
    }

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

    private void HandleEffect(Effect effect, Entity target, int potency)
    {
        if (effect == null) return;

        if (IsStatusEffect(effect))
        {
            ApplyStatusEffect(ResolveStatusType(effect), potency);
            return;
        }

        if (effect.effectType == EffectType.Damage && TryConsumeParry()) return;

        effect.ApplyEffect(target, potency);
    }

    private void ApplyStatusEffect(StatusEffectType effectType, int potency)
    {
        StatusEffectInfo effectInfo = StatusEffectManager.Instance.GetStatusEffectInfo(effectType);
        int stackDelta = Mathf.Max(1, potency);
        float duration = effectInfo != null ? effectInfo.duration : 0f;
        int maxStacks = effectInfo != null ? Mathf.Max(1, effectInfo.maxStacks) : 1;

        StatusEffect existing = FindStatusEffect(effectType);
        if (existing != null)
        {
            existing.currentStacks = Mathf.Min(existing.currentStacks + stackDelta, existing.maxStacks);
            if (duration > 0f)
            {
                existing.timer = Mathf.Max(existing.timer, duration);
            }

            return;
        }

        StatusEffect newStatus = new StatusEffect
        {
            effectType = effectType,
            currentStacks = Mathf.Min(stackDelta, maxStacks),
            maxStacks = maxStacks,
            timer = duration,
        };

        newStatus.onStatusEffectTimedOut += OnStatusEffectTimedOut;
        activeStatusEffects.Add(newStatus);
    }

    private bool TryConsumeParry()
    {
        StatusEffect parry = FindStatusEffect(StatusEffectType.Parry);
        if (parry == null)
        {
            return false;
        }

        parry.currentStacks--;
        if (parry.currentStacks <= 0)
        {
            activeStatusEffects.Remove(parry);
        }

        return true;
    }

    private void TickStatusEffects(float deltaTime)
    {
        foreach (StatusEffect status in activeStatusEffects)
        {
            status.Tick(deltaTime);
        }
    }

    private void OnStatusEffectTimedOut(StatusEffectType effectType)
    {
        // Remove 1 stack of status effect
        var statusEffect = FindStatusEffect(effectType);
        if (statusEffect != null)
        {
            statusEffect.currentStacks--;
            if (statusEffect.currentStacks <= 0)
            {
                activeStatusEffects.Remove(statusEffect);
            }
        }
    }

    private StatusEffect FindStatusEffect(StatusEffectType effectType)
    {
        for (int i = 0; i < activeStatusEffects.Count; i++)
        {
            StatusEffect status = activeStatusEffects[i];
            if (status.effectType == effectType)
            {
                return status;
            }
        }

        return null;
    }

    private static bool IsStatusEffect(Effect effect)
    {
        return effect.effectType == EffectType.Shield || effect is ParryEffect;
    }

    private static StatusEffectType ResolveStatusType(Effect effect)
    {
        if (effect is ParryEffect)
        {
            return StatusEffectType.Parry;
        }

        // Default mapping for shield-type effects until more status-specific assets are added.
        return StatusEffectType.Parry;
    }
}
