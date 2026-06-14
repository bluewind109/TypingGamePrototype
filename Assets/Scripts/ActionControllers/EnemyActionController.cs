using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyActionController : MonoBehaviour, IActionController
{
    [SerializeField] private ActionConfig config;
    [SerializeField] private Timer actionTimer;

	[SerializeField] private float actionDelay = 10f;
	[SerializeField] private List<CombatAction> attackPattern = new List<CombatAction>();
	[SerializeField] private Player playerTarget;

	[Header("Action Bar UI")]
	[SerializeField] private EnemyActionBar actionBar;

	private int patternIndex;
	private Enemy enemy;
	private EffectHandler effectHandler;

	void Awake()
	{
		enemy = GetComponent<Enemy>();
		effectHandler = GetComponent<EffectHandler>();
		actionBar.onTurnEnd += OnTurnEnd;
		actionTimer.onTimerComplete += ExecuteNextAction;
	}
	
	void OnDestroy()
	{
		actionBar.onTurnEnd -= OnTurnEnd;
		actionTimer.onTimerComplete -= ExecuteNextAction;
	}

	private void Update()
	{
		if (enemy == null || effectHandler == null || playerTarget == null) return;
	}

	public void OnActionSelected(CombatAction action)
	{
		// Enemy does not use manual action selection.
	}

	public void Deactivate()
	{
		actionTimer.StopTimer();
	}

	public void StartTurn()
	{
		actionBar.Initialize(config);
		_ = actionBar.InitializeTurn(attackPattern);
		patternIndex = 0;
		SetNextAction();
	}

	private void ExecuteNextAction()
	{
		CombatAction nextAction = GetNextAction();
		if (nextAction == null) return;

		effectHandler.HandleAction(nextAction, enemy, playerTarget);
		actionBar.OnActionExecuted();
		SetNextAction();
	}

	private void SetNextAction()
	{
		actionTimer.StartTimer(actionDelay);
	}

	private CombatAction GetNextAction()
	{
		if (attackPattern.Count == 0) return config != null ? config.basicAttack : null;

		CombatAction action = attackPattern[patternIndex];
		patternIndex = (patternIndex + 1) % attackPattern.Count;
		return action;
	}

	private void OnTurnEnd()
	{
		actionTimer.StopTimer();
		StartTurn();
	}
}
