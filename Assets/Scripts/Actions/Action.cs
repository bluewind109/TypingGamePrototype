using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    [SerializeField] private List<ActionTag> tags = new List<ActionTag>();
    public string actionName;
    public Sprite actionIcon;

    public IReadOnlyList<ActionTag> Tags => tags;

    protected virtual void OnEnable()
    {
        if (tags == null)
        {
            tags = new List<ActionTag>();
        }

        ConfigureTags();
    }

    public bool HasTag(ActionTag tag)
    {
        return tags.Contains(tag);
    }

    protected void EnsureTag(ActionTag tag)
    {
        if (!tags.Contains(tag))
        {
            tags.Add(tag);
        }
    }

    protected virtual void ConfigureTags()
    {
    }

    public abstract void Execute();
}
