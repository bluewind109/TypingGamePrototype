using System;
using UnityEngine;

[RequireComponent(typeof(PlayerActionController))]
public class Player : Entity
{
    public Action<int> onActionPointsChanged;

    private PlayerActionController actionController;

    private int actionPoints = 0;
    public int ActionPoints => actionPoints;

    public bool IsResolved { get; private set; } = false;

    private const int MAX_ACTION_POINTS = 3;

    protected override void Awake()
    {
        base.Awake();
        actionController = GetComponent<PlayerActionController>();
        actionController.onBasicActionExecuted += OnBasicActionExecuted;
        actionController.onSkillUsed += OnSkillUsed;
        onActionPointsChanged += actionController.UpdateAP;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        actionController.onBasicActionExecuted -= OnBasicActionExecuted;
        actionController.onSkillUsed -= OnSkillUsed;
        onActionPointsChanged -= actionController.UpdateAP;
    }


    private void OnBasicActionExecuted()
    {
        actionPoints = Mathf.Min(actionPoints + 1, MAX_ACTION_POINTS);
        onActionPointsChanged?.Invoke(actionPoints);
        // Debug.Log("Gained 1 AP. Current AP: " + actionPoints);
    }

    private void OnSkillUsed(int apCost)
    {
        actionPoints = Mathf.Max(actionPoints - apCost, 0);
        onActionPointsChanged?.Invoke(actionPoints);
        // Debug.Log("Used skill with AP cost: " + apCost + ". Current AP: " + actionPoints);
    }


}
