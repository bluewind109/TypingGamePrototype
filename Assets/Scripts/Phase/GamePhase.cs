using UnityEngine;

public abstract class GamePhase
{
	private Player player;
	private Enemy currentEnemy;

	public GamePhase(Player player, Enemy enemy)
	{
		this.player = player;
		this.currentEnemy = enemy;
	}

    public abstract void Begin();
	public abstract void Update();
	public abstract void End();
}
