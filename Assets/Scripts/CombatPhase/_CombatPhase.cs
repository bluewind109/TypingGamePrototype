using UnityEngine;

public abstract class CombatPhase
{
	protected Player _player;
	protected Enemy _currentEnemy;

	protected float _timeScale = 1f;

	public CombatPhase(Player player, Enemy currentEnemy)
	{
		_player = player;
		_currentEnemy = currentEnemy;
	}

	public abstract void Enter();
	public abstract void Update();
	public abstract void Exit();

	protected float GetEnemyActionTimerRemainingPercentage()
	{
		if (_currentEnemy == null) return 0f;
		return _currentEnemy.GetActionTimerRemainingPercentage();
	}
}
