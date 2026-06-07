using System;
using UnityEngine;

[RequireComponent(typeof(PlayerActionController)), RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public Action<int> onActionPointsChanged;

    [SerializeField] private Stats stats;
    [SerializeField] private PlayerActionController actionController;

    private Health health;
    private int actionPoints = 0;
    public int ActionPoints => actionPoints;

    private const int MAX_ACTION_POINTS = 3;

    private void Awake()
    {
        health = new Health(stats.health);
        health.onDie += OnDie;
        actionController.onBasicActionExecuted += OnBasicActionExecuted;
        actionController.onSkillUsed += OnSkillUsed;
        onActionPointsChanged += actionController.UpdateAP;
    }

    private void OnDestroy()
    {
        health.onDie -= OnDie;
        actionController.onBasicActionExecuted -= OnBasicActionExecuted;
        actionController.onSkillUsed -= OnSkillUsed;
        onActionPointsChanged -= actionController.UpdateAP;
    }

    public void TakeDamage(int amount)
    {
        health.TakeDamage(amount);
    }

    public void Heal(int amount)
    {
        health.Heal(amount);
    }

    private void OnDie()
    {
        Debug.Log("Player died");
        gameObject.SetActive(false);
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
