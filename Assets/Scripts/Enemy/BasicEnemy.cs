
using UnityEngine;

public class BasicEnemy : Enemy
{
	public override void TakeDamage(int amount)
	{
		base.TakeDamage(amount);
		Debug.Log("Basic Enemy took " + amount + " damage");
	}

	protected override void OnDie()
	{
		base.OnDie();
		Debug.Log("Basic Enemy died");
	}
}
