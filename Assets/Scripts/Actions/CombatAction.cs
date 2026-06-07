using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatAction", menuName = "CombatActions")]
public class CombatAction : ScriptableObject
{
    public List<ActionTag> tags = new List<ActionTag>();
    public string actionName;
    public Sprite actionIcon;
    public List<EffectInfo> effects = new List<EffectInfo>();

    public IReadOnlyList<ActionTag> Tags => tags;

    protected virtual void OnEnable()
    {
        if (tags == null)
        {
            tags = new List<ActionTag>();
        }

    }

    public bool HasTag(ActionTag tag)
    {
        return tags.Contains(tag);
    }

    public void Execute()
    {
        foreach (var effect in effects)
        {
            // Implement effect application logic here
            effect.effect.ApplyEffect(); // Replace null with the actual target GameObject
        }
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
}
