using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : GameState
{
	private CombatPhase _currentPhase;
	private NormalPhase _normalPhase;
	private DefendPhase _defendPhase;
	private ResultPhase _resultPhase;

	private Player _player;
	private Enemy _currentEnemy;

	private bool _isInitialized = false;

	private List<Skill> _typedSkills_NormalPhase = new List<Skill>();
	private List<Skill> _typedSkills_DefendPhase = new List<Skill>();
	private Skill _enemySkill;

	private List<Skill> _skillTimeline = new List<Skill>();

	public GameplayState(Player player, Enemy currentEnemy)
	{
		_player = player;
		_currentEnemy = currentEnemy;
	}

	public override void Enter()
	{
		Debug.Log("Enter GameplayState");
		InitPhases();
	}

	private void InitPhases()
	{
		if (_isInitialized) return;
		_normalPhase = new NormalPhase(_player, _currentEnemy, OnNormalPhaseCompleted);
		_defendPhase = new DefendPhase(_player, _currentEnemy, OnDefendPhaseCompleted);
		_resultPhase = new ResultPhase(_player, _currentEnemy);

		SetPhase(_normalPhase);
		_isInitialized = true;
	}

	private void SetPhase(CombatPhase newPhase)
	{
		if (newPhase == null)
		{
			Debug.LogError("New phase is null!");
			return;
		}
		if (_currentPhase == newPhase) return;

		_currentPhase?.Exit();
		_currentPhase = newPhase;
		_currentPhase.Enter();
	}

	public override void Update()
	{
		_currentPhase?.Update();
	}

	public override void Exit()
	{
		Debug.Log("Exit GameplayState");
	}

	private void OnNormalPhaseCompleted(List<Skill> typedSkills)
	{
		_typedSkills_NormalPhase = typedSkills;
		SetPhase(_defendPhase);
	}

	private void OnDefendPhaseCompleted(List<Skill> typedSkills, Skill enemySkill)
	{
		_typedSkills_DefendPhase = typedSkills;
		_enemySkill = enemySkill;
		Debug.Log($"DefendPhase completed. Typed Skills: {string.Join(", ", typedSkills.ConvertAll(skill => skill.name))}, Enemy Skill: {enemySkill?.name}");

		_skillTimeline.Clear();
		_skillTimeline.AddRange(_typedSkills_NormalPhase);

		bool hasDefendSkill = HasDefendSkillTyped(_typedSkills_DefendPhase);

		List<Skill> skillsUpToDefend = hasDefendSkill ? GetSkillsUpToDefendSkill(_typedSkills_DefendPhase) : _typedSkills_DefendPhase;
		List<Skill> skillsAfterDefend = hasDefendSkill ? GetSkillsAfterDefendSkill(_typedSkills_DefendPhase) : new List<Skill>();

		_skillTimeline.AddRange(skillsUpToDefend);
		if (_enemySkill != null) _skillTimeline.Add(_enemySkill);
		_skillTimeline.AddRange(skillsAfterDefend);

		string skillTimelineString = string.Join(", ", _skillTimeline.ConvertAll(skill => skill.name));
		Debug.Log($"Full Skill Timeline: {skillTimelineString}");

		_resultPhase.SetSkillTimeline(_skillTimeline);
		SetPhase(_resultPhase);
	}

	private void OnResultPhaseCompleted()
	{

	}

	private List<Skill> GetSkillsUpToDefendSkill(List<Skill> skills)
	{
		if (skills == null) return new List<Skill>();

		for (int i = 0; i < skills.Count; i++)
		{
			bool isDefendSkill = skills[i].IsDefend();
			if (isDefendSkill)
			{
				return skills.GetRange(0, i + 1);
			}
		}
		return skills;
		// return skills.GetRange(0, defendSkillIndex);
	}

	private List<Skill> GetSkillsAfterDefendSkill(List<Skill> skills)
	{
		if (skills == null) return new List<Skill>();

		for (int i = 0; i < skills.Count; i++)
		{
			bool isDefendSkill = skills[i].IsDefend();
			if (isDefendSkill)
			{
				return skills.GetRange(i + 1, skills.Count - i - 1);
			}
		}

		return new List<Skill>();
	}

	private bool HasDefendSkillTyped(List<Skill> skills)
	{
		if (skills == null) return false;
		Skill defendSkill = _player.GetDefendSkill();
		int defendSkillIndex = skills.IndexOf(defendSkill);
		return defendSkillIndex >= 0;
	}
}