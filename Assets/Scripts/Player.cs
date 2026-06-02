using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Stats stats;

    private Health health;

    private void Awake()
    {
        health = new Health(stats.health);
        health.onDie += OnDie;
    }

    private void OnDestroy()
    {
        if (health != null)
            health.onDie -= OnDie;
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

}
