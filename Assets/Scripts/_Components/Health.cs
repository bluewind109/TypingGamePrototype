using UnityEngine;

public class Health : MonoBehaviour
{
    public System.Action onDie;
    public System.Action<int> onHealthChanged;

    [SerializeField] private Health_UI _healthUI;

    public int CurrentHealth;
    public int MaxHealth;

    public void Initialize(int maxHealth)
    {
        this.MaxHealth = maxHealth;
        this.CurrentHealth = maxHealth;
        _healthUI?.UpdateHealthText(CurrentHealth);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        onHealthChanged?.Invoke(CurrentHealth);
        _healthUI?.UpdateHealthText(CurrentHealth);
        Debug.Log($"Health: {CurrentHealth}/{MaxHealth}");
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            onDie?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        onHealthChanged?.Invoke(CurrentHealth);
        _healthUI?.UpdateHealthText(CurrentHealth);
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
    }
}
