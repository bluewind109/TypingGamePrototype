
using UnityEngine;

public class GameplayState : GameState
{
	private Player _player;
	private Enemy _currentEnemy;

	public GameplayState(Player player, Enemy currentEnemy)
	{
		_player = player;
		_currentEnemy = currentEnemy;
	}

	public override void Enter()
	{
		Debug.Log("Enter GameplayState");
	}

	public override void Update()
	{
		_player?.UpdateEntity();
		_currentEnemy?.UpdateEntity();
	}

	public override void Exit()
	{
		Debug.Log("Exit GameplayState");
	}
}