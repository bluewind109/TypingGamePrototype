using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	[SerializeField] private Stats stats;
	private Health health;

	private void Awake()
	{
		if (stats != null)
		{
			health = new Health(stats.health);
			health.onDie += OnDie;
		}
	}

	private void OnDestroy()
	{
		health.onDie -= OnDie;
	}

	public virtual void TakeDamage(int amount)
	{
		health.TakeDamage(amount);
	}

	protected virtual void OnDie()
	{
		Debug.Log("Enemy died");
		gameObject.SetActive(false);
	}
}
