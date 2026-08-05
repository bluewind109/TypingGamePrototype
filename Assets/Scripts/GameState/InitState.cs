using UnityEngine;

public class InitState : GameState
{
	private bool _isInitialized = false;
	private Player _player;
	private Enemy _currentEnemy;
	private Timer _actionTimer;

	public InitState(Player player, Enemy currentEnemy, Timer actionTimer)
	{
		_player = player;
		_currentEnemy = currentEnemy;
		_actionTimer = actionTimer;
	}

	public override void Enter()
	{
		Debug.Log("Enter InitState");
		_currentEnemy?.Initialize(_player, _actionTimer);
		_isInitialized = true;
	}

	public override void Update()
	{
		if (_isInitialized)
		{
			Exit();
		}
	}

	public override void Exit()
	{
		Debug.Log("Exit InitState");
		GameManager.Instance.EnterGameplayState();
	}
}