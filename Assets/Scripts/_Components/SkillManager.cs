using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private ActionBar actionBar;

    private List<Skill> _actionQueue = new List<Skill>();

    public Skill GetCurrentSkill()
    {
        if (_actionQueue.Count > 0)
        {
            Skill skill = _actionQueue[0];
            _actionQueue.RemoveAt(0);
            return skill;
        }
        else
        {
            Debug.LogWarning("No actions in the queue.");
            return null;
        }
    }

    public void SetNextAction()
    {
        if (_actionQueue.Count == 0) return;

        int firstActionIndex = 0;
        Skill activeAction = _actionQueue[firstActionIndex];
        actionBar.SetActiveAction();
    }
}
