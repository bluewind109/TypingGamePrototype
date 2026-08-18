using UnityEngine;
using System;
using System.Collections.Generic;

public class DefendPhase : CombatPhase
{
	public event Action<List<Skill>> DefendPhaseCompleted;

	public DefendPhase(Player player, Enemy currentEnemy, Action<List<Skill>> defendPhaseCompleted) : base(player, currentEnemy)
	{
		player.SkillsTyped += OnSkillsTyped;
		DefendPhaseCompleted += defendPhaseCompleted;
		_timeScale = 0.5f; // Slow down the enemy's action timer during the DefendPhase
	}

	public override void Enter()
	{
		Debug.Log("Enter <color=green>DefendPhase</color>");
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
		_player?.UpdateEntity();
		_currentEnemy?.UpdateEntity(_timeScale);

		float remainingPercentage = GetEnemyActionTimerRemainingPercentage();
		if (remainingPercentage <= 0.01f)
		{
			List<Skill> typedSkills = _player.GetTypedSkills();
			DefendPhaseCompleted?.Invoke(typedSkills);
		}
	}

	private void OnSkillsTyped(List<Skill> typedSkills)
	{
		if (typedSkills == null) return;
		DefendPhaseCompleted?.Invoke(typedSkills);
	}
}