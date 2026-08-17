using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skills")]
public class Skill : ScriptableObject
{
    public List<SkillTag> Tags = new List<SkillTag>();
    public string Name;
    public Sprite Icon;
    public List<Effect> Effects = new List<Effect>();
    [Min(0)]
    public int ApCost = 0;

	public Sprite GetIcon() => Icon != null ? Icon : null;

	private bool HasTag(SkillTag tag) => Tags.Contains(tag);

	public bool IsBasic() => HasTag(SkillTag.Basic);
	public bool IsAdvanced() => HasTag(SkillTag.Advanced);
	public bool IsAttack() => HasTag(SkillTag.Attack);
	public bool IsDefend() => HasTag(SkillTag.Defend);
}

public enum SkillTag
{
    Basic = 0,
    Attack = 1,
    Defend = 2,
    Advanced = 3,
}