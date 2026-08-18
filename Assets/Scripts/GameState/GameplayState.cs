using System.Collections.Generic;
using UnityEngine;

public class GameplayState : GameState
{
	private CombatPhase _currentPhase;
	private NormalPhase _normalPhase;
	private DefendPhase _defendPhase;
	private ResultPhase _resultPhase;

	private Player _player;
	private Enemy _currentEnemy;

	private bool _isInitialized = false;

	private List<Skill> _typedSkills_NormalPhase = new List<Skill>();
	private List<Skill> _typedSkills_DefendPhase = new List<Skill>();
	private Skill _enemySkill;

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
		_normalPhase = new NormalPhase(_player, _currentEnemy, OnNormalPhaseCompleted);
		_defendPhase = new DefendPhase(_player, _currentEnemy, OnDefendPhaseCompleted);
		_resultPhase = new ResultPhase(_player, _currentEnemy);

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

	private void OnNormalPhaseCompleted(List<Skill> typedSkills)
	{
		_typedSkills_NormalPhase = typedSkills;
		SetPhase(_defendPhase);
	}

	private void OnDefendPhaseCompleted(List<Skill> typedSkills, Skill enemySkill)
	{
		_typedSkills_DefendPhase = typedSkills;
		_enemySkill = enemySkill;

		SetPhase(_resultPhase);
	}

	private void OnResultPhaseCompleted()
	{
		
	}
}