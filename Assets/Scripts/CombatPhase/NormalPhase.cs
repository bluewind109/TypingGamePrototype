using UnityEngine;
using System;

public class NormalPhase : CombatPhase
{
	public event Action DefendPhaseThresholdReached;

	private const float DEFEND_PHASE_THRESHOLD = 0.3f;
	private bool _isDefendPhaseTriggered = false;

	public NormalPhase(Player player, Enemy currentEnemy) : base(player, currentEnemy)
	{
		_timeScale = 1f; // Normal time scale for the NormalPhase
	}

	public override void Enter()
	{
		Debug.Log("Enter <color=green>NormalPhase</color>");
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
		_player?.UpdateEntity();
		_currentEnemy?.UpdateEntity(_timeScale);

		float remainingPercentage = GetEnemyActionTimerRemainingPercentage();
		bool canEnterDefendPhase = remainingPercentage > 0.01f && remainingPercentage <= DEFEND_PHASE_THRESHOLD;
		if (canEnterDefendPhase && !_isDefendPhaseTriggered)
		{
			_isDefendPhaseTriggered = true;
			DefendPhaseThresholdReached?.Invoke();
		}
	}
}