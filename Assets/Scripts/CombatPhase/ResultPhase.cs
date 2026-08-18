using System;
using System.Collections.Generic;
using UnityEngine;

public class ResultPhase : CombatPhase
{
	private List<Skill> _skillTimeline;

	public ResultPhase(Player player, Enemy currentEnemy) : base(player, currentEnemy)
	{
	}

	public void SetSkillTimeline(List<Skill> skillTimeline)
	{
		_skillTimeline = skillTimeline;
	}

	public override void Enter()
	{
		Debug.Log("Enter <color=green>ResultPhase</color>");		
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
	}
}