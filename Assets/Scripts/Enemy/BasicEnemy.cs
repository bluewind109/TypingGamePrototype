
using UnityEngine;

public class BasicEnemy : Enemy
{
	private Skill _basicAttack;

	protected override void SetupSkills()
	{
		base.SetupSkills();
		_basicAttack = Config.basicSkills[0];
	}

	public override void UpdateEntity(float timeScale)
	{
		base.UpdateEntity(timeScale);
	}

	protected override void OnActionTimerComplete()
	{
		base.OnActionTimerComplete();
		PlaySkill(_basicAttack);
		StartActionTimer();
	}
}
