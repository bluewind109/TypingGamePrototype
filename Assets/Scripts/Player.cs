using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IHealable, IShieldable
{
	public event Action<List<Skill>> SkillsTyped;
	public event Action SkillExecuted;

	[SerializeField] protected Stats _stats;
	[SerializeField] private Health _health;
	[SerializeField] private PlayerActionController _actionController;
	[SerializeField] private Enemy _enemy;

	private Animator _animator;

	private int _actionPoints = 0;
	public int ActionPoints => _actionPoints;
	[SerializeField] private ActionPoint_UI _actionPointUI;

	private Vector3 _originalPosition;

	public bool IsResolved { get; private set; } = false;

	private const int MAX_ACTION_POINTS = 3;

	void Awake()
	{
		if (_stats != null && _health != null)
		{
			_health.Initialize(_stats.health);
			_health.onDie += OnDie;
		}

		_animator = GetComponent<Animator>();
		_originalPosition = transform.position;
	}

	void Start()
	{
		if (_actionController != null)
		{
			_actionController.Init(OnSkillsTyped);
		}
		_actionPointUI.UpdateUI(_actionPoints);
	}

	void OnDestroy()
	{
		_health.onDie -= OnDie;
		_actionController.SkillsTyped -= OnSkillsTyped;
	}

	public void UpdateEntity()
	{
		_actionController?.UpdateController();
	}

	private List<Effect> _currentEffects = new List<Effect>();
	private Effect _effectToApply;
	/// <summary>
	/// - Check if the skill is basic or advanced.<br/>
	/// - If basic, execute the skill and increase AP.<br/>
	/// - If advanced, check if the player has enough AP to use it.<br/>
	/// - If enough AP, execute the skill and decrease AP.<br/>
	/// </summary>
	public void ExecuteSkill(Skill skill)
	{
		if (skill == null) return;
		Skill clonedSkill = skill;

		Debug.Log($"Player is executing skill: {clonedSkill.Name}");
		if (clonedSkill.IsBasic())
		{
			UpdateActionPoint(1);
		}
		else if (clonedSkill.IsAdvanced())
		{
			bool hasEnoughAP = ActionPoints >= clonedSkill.ApCost;
			if (!hasEnoughAP) return;
			UpdateActionPoint(-clonedSkill.ApCost);
		}

		_currentEffects = clonedSkill.Effects;
		if (_currentEffects == null || _currentEffects.Count == 0) return;
		_effectToApply = _currentEffects[0];
		if (_effectToApply.IsDamageEffect())
		{
			AnimateAttackSkill();
		}
	}

	private void MoveToPosition(Vector3 targetPosition, float duration, Action onComplete)
	{
		StartCoroutine(MoveCoroutine(targetPosition, duration, onComplete));
	}

	private async Awaitable MoveCoroutine(Vector3 targetPosition, float duration, Action onComplete)
	{
		float elapsedTime = 0f;
		Vector3 startingPosition = transform.position;

		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime / duration);
			transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
			await Awaitable.NextFrameAsync();
		}

		transform.position = targetPosition;
		onComplete?.Invoke();
	}

	/// <summary>
	/// - Move player to near enemy position.<br/>
	/// - Play attack animation.<br/>
	/// - Return to original position.<br/>
	/// </summary>
	private void AnimateAttackSkill()
	{
		MoveToPosition(_enemy.PlayerAttackPosition.position, 0.5f, () =>
		{
			PlayAnimation("Attack");
		});
	}

	private void OnAttackAnimationEventTriggered()
	{
		ApplyEffect(_effectToApply);
	}

	private void ApplyEffect(Effect effect)
	{
		if (effect == null) return;
		Debug.Log($"Applying effect: {effect.type} with potency: {effect.potency}");

		GameObject target = GetTarget(effect.targetTeam);
		effect.Apply(target);
	}


	private void OnAttackAnimationCompleted()
	{
		Debug.Log("Attack animation completed");
		MoveToPosition(_originalPosition, 0.5f, () =>
		{
			_currentEffects.RemoveAt(0);
			if (_currentEffects.Count > 0)
			{
				Debug.Log("Get next effect to apply");
				_effectToApply = _currentEffects[0];
				if (_effectToApply.IsDamageEffect())
				{
					AnimateAttackSkill();
				}
			}
			else
			{
				Debug.Log("Skill execution completed");
				SkillExecuted?.Invoke();
			}
		});
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
				return _enemy.gameObject;
			default:
				return null;
		}
	}

	private void OnDie()
	{
		Debug.Log("Player died");
		// gameObject.SetActive(false);
	}

	public void TakeDamage(int potency)
	{
		_health.TakeDamage(potency);
	}

	public void Heal(int potency)
	{
		_health.Heal(potency);
	}

	public void ReceiveShield(int potency)
	{
		// Implement shield logic here
	}

	public void UpdateActionPoint(int amount)
	{
		_actionPoints = Mathf.Clamp(_actionPoints + amount, 0, MAX_ACTION_POINTS);
		_actionPointUI.UpdateUI(_actionPoints);
	}

	private void OnSkillsTyped(List<Skill> typedSkills)
	{
		SkillsTyped?.Invoke(typedSkills);
	}

	public List<Skill> GetTypedSkills()
	{
		return _actionController.GetTypedSkills();
	}

	public Skill GetDefendSkill()
	{
		return _actionController.GetDefendSkill();
	}

	private void PlayAnimation(string animationName)
	{
		if (_animator == null) return;
		if (string.IsNullOrEmpty(animationName)) return;

		_animator.Play(animationName);
	}
}
