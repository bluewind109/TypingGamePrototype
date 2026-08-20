using System;
using System.Collections.Generic;
using UnityEngine;

public class ResultPhase : CombatPhase
{
	public event Action ResultPhaseCompleted;

	private List<TimelineEntry> _skillTimeline;

	public ResultPhase(Player player, Enemy currentEnemy) : base(player, currentEnemy)
	{
	}

	public void SetSkillTimeline(List<TimelineEntry> skillTimeline)
	{
		_skillTimeline = skillTimeline;
	}

	public override void Enter()
	{
		Debug.Log("Enter <color=green>ResultPhase</color>");

		if (_skillTimeline == null || _skillTimeline.Count == 0)
		{
			Debug.LogWarning("Skill timeline is empty. ResultPhase will complete immediately.");
			ResultPhaseCompleted?.Invoke();
			return;
		}
		
		ExecuteNextSkill();
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
	}

	public void ExecuteNextSkill()
	{
		if (_skillTimeline.Count == 0) return;

		TimelineEntry currentEntry = _skillTimeline[0];
		Skill skillToExecute = currentEntry.Skill;
		SkillSource source = currentEntry.Source;

		if (source == SkillSource.Player)
		{
			Debug.Log($"Executing Player's skill: {skillToExecute.Name}");
			_player.ExecuteSkill(skillToExecute);
		}
		else if (source == SkillSource.Enemy)
		{
			Debug.Log($"Executing Enemy's skill: {skillToExecute.Name}");
			_currentEnemy.ExecuteSkill(skillToExecute);
		}
	}

	/// <summary>
	/// - Remove current skill in the timeline.<br/>
	/// - Check if there are more skills in the timeline.<br/>
	/// - If there are more skills, execute the next skill.<br/>
	/// - If there are no more skills, mark the result phase as resolved.<br/>
	/// </summary>
	private void OnSkillExecuted()
	{
		RemoveCurrentSkill();

		if (_skillTimeline.Count == 0)
		{
			ResultPhaseCompleted?.Invoke();
		}
	}

	private void RemoveCurrentSkill()
	{
		if (_skillTimeline.Count == 0) return;
		_skillTimeline.RemoveAt(0);
	}
}

public enum SkillSource
{
	Player,
	Enemy
}

[Serializable]
public class TimelineEntry
{
	public Skill Skill { get; private set; }
	public SkillSource Source { get; private set; }

	public TimelineEntry(Skill skill, SkillSource source)
	{
		Skill = skill;
		Source = source;
	}
}