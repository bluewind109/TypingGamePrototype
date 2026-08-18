using System;
using UnityEngine;

public class ResultPhase : CombatPhase
{
	public ResultPhase(Player player, Enemy currentEnemy) : base(player, currentEnemy)
	{
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