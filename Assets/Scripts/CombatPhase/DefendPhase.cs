using UnityEngine;
using System;

public class DefendPhase : CombatPhase
{
	public event Action DefendPhaseCompleted;

	public DefendPhase(Player player, Enemy currentEnemy) : base(player, currentEnemy)
	{
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
			DefendPhaseCompleted?.Invoke();
		}
	}
}