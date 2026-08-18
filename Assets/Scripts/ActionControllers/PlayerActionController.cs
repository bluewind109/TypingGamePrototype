using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
	public event Action<List<Skill>> SkillsTyped;

	[Header("Components")]
	[SerializeField] private SkillConfig config;
	[SerializeField] private SentenceManager sentenceManager;

	private Player _player;
	private Enemy _enemy;

	void Start()
	{
		sentenceManager.SkillsTyped += OnSkillsTyped;
	}

	void OnDestroy()
	{
		sentenceManager.SkillsTyped -= OnSkillsTyped;
	}

	public void Init(Player player, Enemy enemy, Action<List<Skill>> onSkillsTyped)
	{
		Debug.Log("PlayerActionController Init called");
		this._player = player;
		this._enemy = enemy;

		sentenceManager.Initialize(config.GetAllSkills());
		sentenceManager.ToggleInput(true);
		SkillsTyped += onSkillsTyped;
	}

	public void UpdateController()
	{
		sentenceManager.UpdateGameplay();
	}

	public List<Skill> GetTypedSkills()
	{
		return sentenceManager.GetTypedSkillsAndReset();
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
		SkillsTyped?.Invoke(typedSkills);

		// foreach (Skill skill in typedSkills)
		// {
		// 	if (skill.IsBasic())
		// 	{
		// 		ExecuteSkill(skill);
		// 		_player.UpdateActionPoint(1);
		// 		continue;
		// 	}
		// 	else if (skill.IsAdvanced())
		// 	{
		// 		bool hasEnoughAP = _player.ActionPoints >= skill.ApCost;
		// 		if (!hasEnoughAP) continue;
		// 		ExecuteSkill(skill);
		// 		_player.UpdateActionPoint(-skill.ApCost);
		// 	}
		// }
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
				return _enemy.gameObject;
			default:
				return null;
		}
	}
}
