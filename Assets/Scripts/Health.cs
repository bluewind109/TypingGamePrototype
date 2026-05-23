using UnityEngine;

public class Health
{
    public System.Action Die;
    public System.Action<int> HealthChanged;

    public int currentHealth;
    public int maxHealth;

    public Health(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        HealthChanged?.Invoke(currentHealth);
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}
