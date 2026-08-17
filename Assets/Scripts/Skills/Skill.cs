using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skills")]
public class Skill : ScriptableObject
{
    public List<SkillTag> tags = new List<SkillTag>();
    public string actionName;
    public Sprite actionIcon;
    public List<EffectInfo> effects = new List<EffectInfo>();
    [Min(0)]
    public int apCost = 0;

    public IReadOnlyList<SkillTag> Tags => tags;

    protected virtual void OnEnable()
    {
        if (tags == null)
        {
            tags = new List<SkillTag>();
        }

    }

    private void ResolveEffect(EffectInfo effect, GameObject target)
    {
        if (effect == null || target == null) return;
        switch (effect.type)
        {
            case EffectType.Damage:
                target.GetComponent<IDamageable>()?.TakeDamage(effect.potency);
                break;
            case EffectType.Heal:
                target.GetComponent<IHealable>()?.Heal(effect.potency);
                break;
            case EffectType.Shield:
                target.GetComponent<IShieldable>()?.ReceiveShield(effect.potency);
                break;
            default:
                break;
        }
    }

    public Sprite GetIcon()
    {
        return actionIcon != null ? actionIcon : null;
    }

    public bool HasTag(SkillTag tag)
    {
        return tags.Contains(tag);
    }

    public void IncreaseEffectPotency(int index)
    {
        if (index >= 0 && index < effects.Count)
        {
            effects[index].IncreasePotency();
        }
    }

    public void DecreaseEffectPotency(int index)
    {
        if (index >= 0 && index < effects.Count)
        {
            effects[index].DecreasePotency();
        }
    }

    public bool IsBasic()
    {
        return tags.Contains(SkillTag.Basic);
    }

	public bool IsAdvanced()
	{
		return tags.Contains(SkillTag.Advanced);
	}

    public bool IsAttack()
    {
        return tags.Contains(SkillTag.Attack);
    }

    public bool IsDefend()
    {
        return tags.Contains(SkillTag.Defend);
    }
}
