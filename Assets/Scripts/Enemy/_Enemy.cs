using System;
using UnityEngine;

[RequireComponent(typeof(EnemyActionController))]
public abstract class Enemy : Entity
{
	private EnemyActionController actionController;

	protected override void Awake()
	{
		base.Awake();
		actionController = GetComponent<EnemyActionController>();
	}

	void Start()
	{
		OnSpawn();
	}

	public virtual void OnSpawn()
	{
		actionController.StartTurn();
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
	}
}
