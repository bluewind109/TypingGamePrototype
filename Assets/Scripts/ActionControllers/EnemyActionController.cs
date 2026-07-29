using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyActionController : MonoBehaviour, IActionController
{
	public Action onPatternFinished;

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
		actionTimer.onTimerComplete += ExecuteNextAction;
	}

	void OnDestroy()
	{
		actionTimer.onTimerComplete -= ExecuteNextAction;
	}

	public void StartTurn()
	{
		_ = actionBar.InitializeTurn(attackPattern);
		patternIndex = 0;
	}

	public void StartCombat()
	{
		StartNextAction();
	}

	public void EndTurn()
	{
		// TODO Resolve any end-of-turn effects here if needed.
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

	private void ExecuteNextAction()
	{
		CombatAction nextAction = GetNextAction();
		if (nextAction == null) return;

		effectHandler.HandleAction(nextAction, enemy, playerTarget);
		actionBar.OnActionExecuted();

		if (actionBar.IsPatternFinished())
		{
			onPatternFinished?.Invoke();
			// GameManager.Instance.EndTurn();
			return;
		}

		StartNextAction();
	}

	private void StartNextAction()
	{
		actionBar.SetActiveItem();
		actionTimer.StartTimer(actionDelay);
	}

	private CombatAction GetNextAction()
	{
		if (attackPattern.Count == 0) return config != null ? config.basicAttack : null;

		CombatAction action = attackPattern[patternIndex];
		patternIndex = (patternIndex + 1) % attackPattern.Count;
		return action;
	}
}
