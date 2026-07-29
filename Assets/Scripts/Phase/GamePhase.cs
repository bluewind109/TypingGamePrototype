using UnityEngine;

public abstract class GamePhase
{
	public GamePhase()
	{
		
	}

    public abstract void Begin();
	public abstract void Update();
	public abstract void End();
}
