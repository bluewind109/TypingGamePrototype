
using UnityEngine;

public class BasicEnemy : Enemy
{
	private Skill _basicAttack;

	protected override void SetupSkills()
	{
		base.SetupSkills();
		_basicAttack = Config.basicSkills[0];
	}

	public override void UpdateEntity()
	{
		base.UpdateEntity();
	}

	protected override void OnActionTimerComplete()
	{
		PlaySkill(_basicAttack);
		StartActionTimer();
	}
}
