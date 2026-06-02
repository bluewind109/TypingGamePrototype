using UnityEngine;

public class Health
{
    public System.Action onDie;
    public System.Action<int> onHealthChanged;

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
        onHealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onDie?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        onHealthChanged?.Invoke(currentHealth);
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}
