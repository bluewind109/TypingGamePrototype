using System;
using UnityEngine;

[Serializable]
public class EffectInfo
{
    public EffectType type;
    public int potency; // e.g., amount of damage or healing
    public TargetType targetType;
    public TargetTeam targetTeam;

    public void Apply(GameObject target)
    {
        if (target == null) return;

        switch (type)
        {
            case EffectType.Damage:
                target.GetComponent<IDamageable>()?.TakeDamage(potency);
                break;
            case EffectType.Heal:
                target.GetComponent<IHealable>()?.Heal(potency);
                break;
            case EffectType.Shield:
                target.GetComponent<IShieldable>()?.ReceiveShield(potency);
                break;
            default:
                Debug.LogWarning($"Effect type {type} not implemented.");
                break;
        }
    }

    public void IncreasePotency()
    {
        potency *= 2;
    }

    public void DecreasePotency()
    {
        potency = Mathf.Max(1, potency / 2);
    }
}

public enum EffectType
{
    Damage = 0,
    Heal = 1,
    Shield = 2,
}
