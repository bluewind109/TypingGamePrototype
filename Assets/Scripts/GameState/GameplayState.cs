using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : GameState
{
	private CombatPhase _currentPhase;
	private AttackPhase _attackPhase;
	private DefendPhase _defendPhase;
	private ResultPhase _resultPhase;

	private Player _player;
	private Enemy _currentEnemy;

	private bool _isInitialized = false;

	private List<Skill> _typedSkills_AttackPhase = new List<Skill>();
	private List<Skill> _typedSkills_DefendPhase = new List<Skill>();
	private Skill _enemySkill;

	private List<TimelineEntry> _skillTimeline = new List<TimelineEntry>();

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
		_attackPhase = new AttackPhase(_player, _currentEnemy, OnAttackPhaseCompleted);
		_defendPhase = new DefendPhase(_player, _currentEnemy, OnDefendPhaseCompleted);
		_resultPhase = new ResultPhase(_player, _currentEnemy);

		SetPhase(_attackPhase);
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

	private void OnAttackPhaseCompleted(List<Skill> typedSkills)
	{
		_typedSkills_AttackPhase = typedSkills;
		SetPhase(_defendPhase);
	}

	/// <summary>
	/// - Store typed skills from DefendPhase and the enemy's skill. <br/>
	/// - Add attack phase skills to the skill timeline. <br/>
	/// - Check if a defend skill was typed. <br/>
	/// - If a defend skill was typed, split the defend phase skills into two parts: 
	/// skills up to and including the defend skill, and skills after the defend skill. <br/>
	/// - Move to Result Phase
	/// </summary>
	private void OnDefendPhaseCompleted(List<Skill> typedSkills, Skill enemySkill)
	{
		_typedSkills_DefendPhase = typedSkills;
		_enemySkill = enemySkill;
		Debug.Log($"DefendPhase completed. Typed Skills: {string.Join(", ", typedSkills.ConvertAll(skill => skill.name))}, Enemy Skill: {enemySkill?.name}");

		_skillTimeline.Clear();
		AddSkillsToTimeline(_typedSkills_AttackPhase, SkillSource.Player);

		bool hasDefendSkill = HasDefendSkillTyped(_typedSkills_DefendPhase);

		List<Skill> skillsUpToDefend = hasDefendSkill ? GetSkillsUpToDefendSkill(_typedSkills_DefendPhase) : _typedSkills_DefendPhase;
		List<Skill> skillsAfterDefend = hasDefendSkill ? GetSkillsAfterDefendSkill(_typedSkills_DefendPhase) : new List<Skill>();

		AddSkillsToTimeline(skillsUpToDefend, SkillSource.Player);
		AddSkillToTimeline(_enemySkill, SkillSource.Enemy);
		AddSkillsToTimeline(skillsAfterDefend, SkillSource.Player);

		string skillTimelineString = string.Join(", ", _skillTimeline.ConvertAll(entry => entry.Skill.name));
		Debug.Log($"Full Skill Timeline: {skillTimelineString}");

		_resultPhase.SetSkillTimeline(_skillTimeline);
		SetPhase(_resultPhase);
	}

	private void AddSkillsToTimeline(List<Skill> skills, SkillSource source)
	{
		if (skills == null) return;
		foreach (Skill skill in skills)
		{
			AddSkillToTimeline(skill, source);
		}
	}

	private void AddSkillToTimeline(Skill skill, SkillSource source)
	{
		if (skill == null) return;
		TimelineEntry entry = new TimelineEntry(skill, source);
		_skillTimeline.Add(entry);
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

	private void OnResultPhaseCompleted()
	{

	}
}