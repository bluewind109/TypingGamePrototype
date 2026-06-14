using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyActionController : MonoBehaviour, IActionController
{
    [SerializeField] private ActionConfig config;

	[SerializeField] private float turnDelay = 2f;
	[SerializeField] private List<EnemyActionStep> attackPattern = new List<EnemyActionStep>();
	[SerializeField] private Player playerTarget;

	[Header("Action Bar UI")]
	[SerializeField] private Transform actionBar;
	[SerializeField] private EnemyActionItem actionItemPrefab;

	private float actionTimer;
	private int patternIndex;
	private Enemy enemy;
	private EffectHandler effectHandler;

	private void Awake()
	{
		enemy = GetComponent<Enemy>();
		effectHandler = GetComponent<EffectHandler>();
	}

	private void OnEnable()
	{
		actionTimer = turnDelay;
	}

	private void Update()
	{
		if (enemy == null || effectHandler == null || playerTarget == null) return;

		actionTimer -= Time.deltaTime;
		if (actionTimer > 0f) return;

		actionTimer = turnDelay;
		ExecuteNextAction();
	}

	public void OnActionSelected(CombatAction action)
	{
		// Enemy does not use manual action selection.
	}

	// public void Initialize(Player target)
	// {
	// 	playerTarget = target;
	// }

	public void SetAttackPattern(List<EnemyActionStep> pattern)
	{
		attackPattern = pattern ?? new List<EnemyActionStep>();
		patternIndex = 0;
	}

	private void ExecuteNextAction()
	{
		CombatAction nextAction = GetNextAction();
		if (nextAction == null) return;

		effectHandler.HandleAction(nextAction, enemy, playerTarget);
		SetNextAction();
	}

	private void SetNextAction()
	{
		
	}

	private CombatAction GetNextAction()
	{
		if (attackPattern.Count == 0) return config != null ? config.basicAttack : null;

		EnemyActionStep step = attackPattern[patternIndex];
		patternIndex = (patternIndex + 1) % attackPattern.Count;

		switch (step)
		{
			case EnemyActionStep.BasicAttack:
				return config != null ? config.basicAttack : null;
			case EnemyActionStep.BasicDefend:
				return config != null ? config.basicDefend : null;
			case EnemyActionStep.Skill0:
				return GetSkill(0);
			case EnemyActionStep.Skill1:
				return GetSkill(1);
			default:
				return config != null ? config.basicAttack : null;
		}
	}

	private CombatAction GetSkill(int index)
	{
		if (config == null || config.skills == null || index < 0 || index >= config.skills.Count)
		{
			return null;
		}

		return config.skills[index];
	}
}

public enum EnemyActionStep
{
	BasicAttack = 0,
	BasicDefend = 1,
	Skill0 = 2,
	Skill1 = 3,
}
