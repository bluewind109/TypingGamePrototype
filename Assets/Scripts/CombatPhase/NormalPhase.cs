using UnityEngine;
using System;
using System.Collections.Generic;

public class NormalPhase : CombatPhase
{
	public event Action<List<Skill>> NormalPhaseCompleted;

	private const float DEFEND_PHASE_THRESHOLD = 0.3f;
	private bool _isDefendPhaseTriggered = false;

	public NormalPhase(Player player, Enemy currentEnemy, Action<List<Skill>> normalPhaseCompleted) : base(player, currentEnemy)
	{
		NormalPhaseCompleted += normalPhaseCompleted;
		_timeScale = 1f; // Normal time scale for the NormalPhase
	}

	public override void Enter()
	{
		Debug.Log("Enter <color=green>NormalPhase</color>");
		_player.SkillsTyped += OnSkillsTyped;
	}

	public override void Exit()
	{
		_player.SkillsTyped -= OnSkillsTyped;
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
			List<Skill> typedSkills = _player.GetTypedSkills();
			NormalPhaseCompleted?.Invoke(typedSkills);
		}
	}

	private void OnSkillsTyped(List<Skill> typedSkills)
	{
		if (typedSkills == null) return;
		NormalPhaseCompleted?.Invoke(typedSkills);
	}
}