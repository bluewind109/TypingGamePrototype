using UnityEngine;

public class GameOverState : GameState
{
	public override void Enter()
	{
		Debug.Log("Enter GameOverState");
	}

	public override void Update() { }

	public override void Exit()
	{
		Debug.Log("Exit GameOverState");
	}
}