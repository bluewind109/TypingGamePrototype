using System;
using UnityEngine;

[RequireComponent(typeof(EnemyActionController))]
public abstract class Enemy : Entity
{
	private EnemyActionController actionController;
    public bool IsResolved { get; private set; } = false;

	protected override void Awake()
	{
		base.Awake();
		actionController = GetComponent<EnemyActionController>();
	}

	void Start()
	{
		OnSpawn();
	}

	public override void StartTurn()
	{
		actionController.StartTurn();
	}

	public override void StartCombat()
	{
		actionController.StartCombat();
	}

	public override void EndTurn()
	{
		actionController.EndTurn();
		IsResolved = true;
	}

	public virtual void OnSpawn()
	{
		// StartTurn();
	}

	public override void TakeDamage(int amount)
	{
		base.TakeDamage(amount);
	}

	protected override void OnDie()
	{
		base.OnDie();
		// TODO enemy stop all actions and disable itself
		actionController.Deactivate();
		IsResolved = true;
	}
}
