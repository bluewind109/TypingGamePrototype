using UnityEngine;

public class InitState : GameState
{
	private bool _isInitialized = false;

	public override void Enter()
	{
		Debug.Log("Enter InitState");
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