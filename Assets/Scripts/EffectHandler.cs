using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [SerializeField] private StatusEffect statusEffectPrefab;
    [SerializeField] private Transform statusEffectContainer;

    private List<StatusEffect> statusEffects = new List<StatusEffect>();

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
            existing.UpdateStack(stackDelta);
            return;
        }

        StatusEffect newStatusInstance = Instantiate(statusEffectPrefab, statusEffectContainer);
        newStatusInstance.Initialize(effectType, stackDelta, duration);

        newStatusInstance.onStatusEffectTimedOut += OnStatusEffectTimedOut;
        statusEffects.Add(newStatusInstance);
    }

    private bool TryConsumeParry()
    {
        StatusEffect parry = FindStatusEffect(StatusEffectType.Parry);
        if (parry == null) return false;

        parry.UpdateStack(-1);
        return true;
    }

    private void TickStatusEffects(float deltaTime)
    {
        foreach (StatusEffect status in statusEffects)
        {
            status.UpdateDuration(deltaTime);
        }
    }

    private void OnStatusEffectTimedOut(StatusEffectType effectType)
    {
        // var statusEffect = FindStatusEffect(effectType);
    }

    private StatusEffect FindStatusEffect(StatusEffectType effectType)
    {
        for (int i = 0; i < statusEffects.Count; i++)
        {
            StatusEffect status = statusEffects[i];
            if (status.EffectType == effectType)
            {
                return status;
            }
        }

        return null;
    }

    private bool IsStatusEffect(Effect effect)
    {
        return effect.effectType == EffectType.Shield || effect is ParryEffect;
    }

    private StatusEffectType ResolveStatusType(Effect effect)
    {
        if (effect is ParryEffect)
        {
            return StatusEffectType.Parry;
        }

        // Default mapping for shield-type effects until more status-specific assets are added.
        return StatusEffectType.Parry;
    }
}
