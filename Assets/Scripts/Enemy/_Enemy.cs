using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IHealable, IShieldable
{
	public event Action Die;
	public event Action<Skill> ActionTimerCompleted;

	[SerializeField] private Stats _stats;
	[SerializeField] private Health _health;

	[SerializeField] private Player _player;
	[SerializeField] private SkillConfig _config;
	[SerializeField] private Timer _actionTimer;
	[SerializeField] private float _actionInterval = 5f;

	[SerializeField] private Transform _sentenceContainer;
	[SerializeField] private SentenceDisplay _sentenceDisplayPrefab;

	public Player Player { get => _player; }
	public SkillConfig Config { get => _config; }

	private SkillManager skillManager;
	private List<Skill> _availableSkills;

	private Skill _basicAttack;

	protected virtual void Awake()
	{
		skillManager = GetComponent<SkillManager>();
	}

	public virtual void Initialize(Player player, Timer timer)
	{
		this._player = player;
		this._actionTimer = timer;

		_actionTimer.onTimerComplete += OnActionTimerCompleted;

		if (_stats != null)
		{
			_health.Initialize(_stats.health);
			_health.onDie += OnDie;
		}

		SetupSkills();
		StartActionTimer();
	}

	protected virtual void SetupSkills()
	{
		_availableSkills = _config.GetAllSkills();
		foreach (Skill skill in _availableSkills)
		{
			Sentence sentence = new Sentence(
				skill.Name,
				Instantiate(_sentenceDisplayPrefab, _sentenceContainer)
			);
		}
	}

	protected void StartActionTimer()
	{
		if (_actionTimer == null) return;
		skillManager.SetCurrentSkill(_config.basicSkills[0]);
		_actionTimer.Play(_actionInterval);
	}

	protected virtual void OnDestroy()
	{
		_actionTimer.onTimerComplete -= OnActionTimerCompleted;
	}

	public virtual void UpdateEntity(float timeScale)
	{
		_actionTimer?.UpdateTime(timeScale);
	}

	protected virtual void OnDie()
	{
		Debug.Log("Enemy died");
		_actionTimer.Pause();
		Die?.Invoke();
		// gameObject.SetActive(false);
	}

	public float GetActionTimerRemainingPercentage()
	{
		return _actionTimer.RemainingPercentage;
	}

	protected virtual void OnActionTimerCompleted()
	{
		ActionTimerCompleted?.Invoke(GetCurrentSkill());
	}

	public Skill GetCurrentSkill()
	{
		if (skillManager == null) return null;
		return skillManager.GetCurrentSkill();
	}

	protected void PlaySkill(Skill skill)
	{
		if (skill == null) return;
		foreach (Effect effect in skill.Effects)
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
