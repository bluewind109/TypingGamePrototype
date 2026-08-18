using UnityEngine;

public abstract class CombatPhase
{
	protected Player _player;
	protected Enemy _currentEnemy;

	public CombatPhase(Player player, Enemy currentEnemy)
	{
		_player = player;
		_currentEnemy = currentEnemy;
	}

	public abstract void Enter();
	public abstract void Update();
	public abstract void Exit();
}
