using UnityEngine;

public class GameplayState : GameState
{
	private CombatPhase _currentPhase;
	private NormalPhase _normalPhase;
	private DefendPhase _defendPhase;

	private Player _player;
	private Enemy _currentEnemy;

	private bool _isInitialized = false;

	public GameplayState(Player player, Enemy currentEnemy)
	{
		_player = player;
		_currentEnemy = currentEnemy;
	}

	public override void Enter()
	{
		Debug.Log("Enter GameplayState");
		InitPhases();
	}

	private void InitPhases()
	{
		if (_isInitialized) return;
		_normalPhase = new NormalPhase(_player, _currentEnemy);
		_defendPhase = new DefendPhase(_player, _currentEnemy);

		_normalPhase.DefendPhaseThresholdReached += OnDefendPhaseThresholdReached;
		_defendPhase.DefendPhaseCompleted += OnDefendPhaseCompleted;

		SetPhase(_normalPhase);
		_isInitialized = true;
	}

	private void SetPhase(CombatPhase newPhase)
	{
		if (newPhase == null)
		{
			Debug.LogError("New phase is null!");
			return;
		}
		if (_currentPhase == newPhase) return;

		_currentPhase?.Exit();
		_currentPhase = newPhase;
		_currentPhase.Enter();
	}

	public override void Update()
	{
		_currentPhase?.Update();
	}

	public override void Exit()
	{
		Debug.Log("Exit GameplayState");
	}

	private void OnDefendPhaseThresholdReached()
	{
		SetPhase(_defendPhase);
	}

	private void OnDefendPhaseCompleted()
	{
		SetPhase(_normalPhase);
	}
}