using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] private SkillConfig config;
	[SerializeField] private SentenceManager sentenceManager;

	[Header("References")]
	[SerializeField] private Enemy enemy;

	private Player _player;

	void Awake()
	{
		_player = GetComponent<Player>();
		sentenceManager.onSkillsTyped += OnSkillsTyped;
	}

	void Start()
	{
		sentenceManager.Initialize(config.GetAllSkills());
		sentenceManager.ToggleInput(true);
	}

	void OnDestroy()
	{
		sentenceManager.onSkillsTyped -= OnSkillsTyped;
	}

	public void UpdateController()
	{
		sentenceManager.UpdateGameplay();
	}

	/// <summary>
	/// - Loop through each action in the typed actions list.
	/// - Check if the action is basic or skill.
	/// - If basic, execute the action and increase AP.
	/// - If skill, check if the player has enough AP to use it.
	/// - If enough AP, execute the skill and decrease AP.
	/// </summary>
	/// <param name="typedSkills"></param>
	private void OnSkillsTyped(List<Skill> typedSkills)
	{
		if (typedSkills == null) return;

		foreach (Skill skill in typedSkills)
		{
			if (skill.IsBasic())
			{
				ExecuteAction(skill);
				_player.UpdateActionPoint(1);
				continue;
			}
			else if (skill.IsAdvanced())
			{
				bool hasEnoughAP = _player.ActionPoints >= skill.apCost;
				if (!hasEnoughAP) continue;
				ExecuteAction(skill);
				_player.UpdateActionPoint(-skill.apCost);
			}
		}
	}

	private void ExecuteAction(Skill skill)
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
				return enemy.gameObject;
			default:
				return null;
		}
	}
}
