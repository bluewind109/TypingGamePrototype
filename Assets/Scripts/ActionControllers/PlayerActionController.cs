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
	/// - Loop through each skill in the typed skills list.<br/>
	/// - Check if the skill is basic or advanced.<br/>
	/// - If basic, execute the skill and increase AP.<br/>
	/// - If advanced, check if the player has enough AP to use it.<br/>
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
				ExecuteSkill(skill);
				_player.UpdateActionPoint(1);
				continue;
			}
			else if (skill.IsAdvanced())
			{
				bool hasEnoughAP = _player.ActionPoints >= skill.ApCost;
				if (!hasEnoughAP) continue;
				ExecuteSkill(skill);
				_player.UpdateActionPoint(-skill.ApCost);
			}
		}
	}

	private void ExecuteSkill(Skill skill)
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
				return enemy.gameObject;
			default:
				return null;
		}
	}
}
