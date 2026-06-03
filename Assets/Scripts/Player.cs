using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActionController)), RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private Stats stats;
    [SerializeField] private PlayerActionController actionController;

    private Health health;
    private int actionPoints = 0;

    private void Awake()
    {
        health = new Health(stats.health);
        health.onDie += OnDie;
        actionController = GetComponentInChildren<PlayerActionController>();
        actionController.onBasicActionExecuted += OnBasicActionExecuted;
    }

    private void OnDestroy()
    {
        health.onDie -= OnDie;
        actionController.onBasicActionExecuted -= OnBasicActionExecuted;
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
        actionPoints++;
        Debug.Log("Gained 1 AP. Current AP: " + actionPoints);
    }

}
