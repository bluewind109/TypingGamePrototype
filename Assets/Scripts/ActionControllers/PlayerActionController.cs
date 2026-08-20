using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
	public event Action<List<Skill>> SkillsTyped;

	[Header("Components")]
	[SerializeField] private SkillConfig config;
	[SerializeField] private SentenceManager sentenceManager;

	void Start()
	{
		sentenceManager.SkillsTyped += OnSkillsTyped;
	}

	void OnDestroy()
	{
		sentenceManager.SkillsTyped -= OnSkillsTyped;
	}

	public void Init(Action<List<Skill>> onSkillsTyped)
	{
		Debug.Log("PlayerActionController Init called");

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
	public Skill GetDefendSkill()
	{
		return config.GetDefendSkill();
	}

	private void OnSkillsTyped(List<Skill> typedSkills)
	{
		if (typedSkills == null) return;
		SkillsTyped?.Invoke(typedSkills);
	}
}
