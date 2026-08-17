using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IHealable, IShieldable
{
	public event Action onDie;

	[SerializeField] private Stats _stats;
	[SerializeField] private Health _health;

	[SerializeField] private Player _player;
	[SerializeField] private SkillConfig _config;
	[SerializeField] private Timer _actionTimer;
	[SerializeField] private float _actionInterval = 5f;

	public Player Player { get => _player; }
	public SkillConfig Config { get => _config; }

	private SkillManager skillManager;

	protected virtual void Awake()
	{
		skillManager = GetComponent<SkillManager>();
	}

	public virtual void Initialize(Player player, Timer timer)
	{
		this._player = player;
		this._actionTimer = timer;

		_actionTimer.onTimerComplete += OnActionTimerComplete;

		if (_stats != null)
		{
			_health.Initialize(_stats.health);
			_health.onDie += OnDie;
		}

		SetupSkills();
		StartActionTimer();
	}

	protected abstract void SetupSkills();

	protected void StartActionTimer()
	{
		if (_actionTimer == null) return;
		_actionTimer.Play(_actionInterval);
	}

	protected virtual void OnDestroy()
	{
		_actionTimer.onTimerComplete -= OnActionTimerComplete;
	}

	public virtual void UpdateEntity()
	{
		_actionTimer?.UpdateTime();
	}

	protected virtual void OnDie()
	{
		Debug.Log("Enemy died");
		_actionTimer.Pause();
		// gameObject.SetActive(false);
	}

	/// <summary>
	/// - This method is called when the action timer completes, 
	/// indicating that the enemy's current action has finished executing.
	/// - Get current action and play it
	/// - Get next action in the queue
	/// - Reset the action timer
	/// </summary>
	protected abstract void OnActionTimerComplete();

	protected void PlaySkill(Skill skill)
	{
		if (skill == null) return;
		foreach (EffectInfo effect in skill.effects)
		{
			GameObject target = GetTarget(effect.targetTeam);
			effect.Apply(target);
		}
	}

	private GameObject GetTarget(TargetTeam targetTeam)
	{
		switch (targetTeam)
		{
			case TargetTeam.Self:
				return gameObject;
			case TargetTeam.Ally:
				return null; // Implement ally targeting logic if needed
			case TargetTeam.Enemy:
				return Player.gameObject;
			default:
				return null;
		}
	}

	public virtual void TakeDamage(int potency)
	{
		_health.TakeDamage(potency);
	}

	public virtual void Heal(int potency)
	{
		_health.Heal(potency);
	}

	public virtual void ReceiveShield(int potency)
	{
		// Implement shield logic here
	}
}
