
using UnityEngine;

public class BasicEnemy : Enemy
{
	private Skill basicAttack;

	protected override void SetupSkills()
	{
		basicAttack = Config.basicSkills[0];
	}

	public override void UpdateEntity()
	{
		base.UpdateEntity();
	}

	protected override void OnActionTimerComplete()
	{
		PlaySkill(basicAttack);
		StartActionTimer();
	}
}
