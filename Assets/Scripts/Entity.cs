using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected Stats stats;
    protected Health health;

    protected virtual void Awake()
    {
        if (stats != null)
        {
            health = new Health(stats.health);
            health.onDie += OnDie;
        }
    }

    protected virtual void OnDestroy()
    {
        health.onDie -= OnDie;
    }

    public virtual void TakeDamage(int amount)
    {
        health.TakeDamage(amount);
    }

    public virtual void Heal(int amount)
    {
        health.Heal(amount);
    }

    protected virtual void OnDie()
    {
        Debug.Log("Entity died");
        // gameObject.SetActive(false);
    }
}
