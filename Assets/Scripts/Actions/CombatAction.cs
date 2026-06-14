using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatAction", menuName = "CombatActions")]
public class CombatAction : ScriptableObject
{
    public List<ActionTag> tags = new List<ActionTag>();
    public string actionName;
    public Sprite actionIcon;
    public List<EffectInfo> effects = new List<EffectInfo>();
    [Min(0)]
    public int apCost = 0;

    public IReadOnlyList<ActionTag> Tags => tags;

    protected virtual void OnEnable()
    {
        if (tags == null)
        {
            tags = new List<ActionTag>();
        }

    }

    public Sprite GetIcon()
    {
        return actionIcon != null ? actionIcon : null;
    }

    public bool HasTag(ActionTag tag)
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

    public bool IsBasicAction()
    {
        return tags.Contains(ActionTag.Basic);
    }

    public bool IsAttack()
    {
        return tags.Contains(ActionTag.Attack);
    }

    public bool IsDefend()
    {
        return tags.Contains(ActionTag.Defend);
    }
}
