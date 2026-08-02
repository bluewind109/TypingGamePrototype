using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [SerializeField] private ActionBar actionBar;

    private List<CombatAction> _actionQueue = new List<CombatAction>();

    public CombatAction GetCurrentAction()
    {
        if (_actionQueue.Count > 0)
        {
            CombatAction action = _actionQueue[0];
            _actionQueue.RemoveAt(0);
            return action;
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
        CombatAction activeAction = _actionQueue[firstActionIndex];
        actionBar.SetActiveAction(firstActionIndex);
    }
}
