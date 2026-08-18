using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
	private Skill _currentSkill;

	public Skill GetCurrentSkill()
	{
		return _currentSkill;
	}

	public void SetCurrentSkill(Skill skill)
	{
		_currentSkill = skill;
	}
}
