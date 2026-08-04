using System;
using UnityEngine;

public abstract class Enemy : Entity
{
	public Action onTurnFinished;

	[SerializeField] private Player player;
	[SerializeField] private ActionConfig config;
	[SerializeField] private Timer actionTimer;

	private ActionManager actionManager;

	protected override void Awake()
	{
		base.Awake();
		actionManager = GetComponent<ActionManager>();
	}

	void Start()
	{
		actionTimer.onTimerComplete += OnActionTimerComplete;

		OnSpawn();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		actionTimer.onTimerComplete -= OnActionTimerComplete;
	}

	public virtual void OnSpawn()
	{
		// StartTurn();
	}

	protected override void OnDie()
	{
		base.OnDie();
		// TODO enemy stop all actions and disable itself
	}

	/// <summary>
	/// This method is called when the action timer completes, 
	/// indicating that the enemy's current action has finished executing.
	/// Get current action and play it
	/// Get next action in the queue
	/// If there is next action, reset the action timer
	/// Else send the signal that the enemy has finished its turn
	/// </summary>
	private void OnActionTimerComplete()
	{
		// Get current action and play it
		CombatAction action = actionManager.GetCurrentAction();
		PlayAction(action);
	}

	private void PlayAction(CombatAction action)
	{
		if (action == null) return;
		foreach (EffectInfo effect in action.effects)
		{
			Entity targetEntity = null; ;
			switch (effect.targetTeam)
			{
				case TargetTeam.Self:
					targetEntity = this;
					break;
				case TargetTeam.Ally:
					break;
				case TargetTeam.Enemy:
					targetEntity = player;
					break;
				default:
					break;
			}

			action.Use(targetEntity);
		}
	}

	// public async Task InitializeTurn(List<CombatAction> upcomingActions)
	// {
	// 	ClearImmediateItems();

	// 	if (upcomingActions == null || upcomingActions.Count == 0) return;

	// 	for (var i = 0; i < upcomingActions.Count; i++)
	// 	{
	// 		CombatAction action = upcomingActions[i];
	// 		AddAction(action);
	// 		await Task.Delay(250);
	// 	}
	// 	SetActiveAction();
	// }
}
