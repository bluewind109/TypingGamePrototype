using System;
using UnityEngine;

[RequireComponent(typeof(EnemyActionController))]
public abstract class Enemy : Entity
{
	private EnemyActionController actionController;
	[SerializeField] private TMPro.TextMeshProUGUI healthText;

	protected override void Awake()
	{
		base.Awake();
		actionController = GetComponent<EnemyActionController>();
		UpdateHealthText();
	}

	public override void TakeDamage(int amount)
	{
		base.TakeDamage(amount);
		UpdateHealthText();
	}

	private void UpdateHealthText()
	{
		if (healthText == null) return;
		healthText.text = health.currentHealth.ToString();
	}

	protected override void OnDie()
	{
		base.OnDie();
		// TODO enemy stop all actions and disable itself

	}
}
