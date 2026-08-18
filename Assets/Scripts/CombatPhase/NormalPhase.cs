
public class NormalPhase : CombatPhase
{
	public NormalPhase(Player player, Enemy currentEnemy) : base(player, currentEnemy)
	{
	}

	public override void Enter()
	{
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
		_player?.UpdateEntity();
		_currentEnemy?.UpdateEntity();
	}
}