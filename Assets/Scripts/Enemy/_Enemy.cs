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

	protected override void OnDie()
	{
		base.OnDie();
		// TODO enemy stop all actions and disable itself

	}
}
