using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected Stats stats;
    public Health health { get; protected set; }

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
        if (health != null)
        {
            health.onDie -= OnDie;
        }
    }

    public virtual void PlayAction()
    {
        
    }

    public virtual void TakeDamage(int potency)
    {
        health.TakeDamage(potency);
    }

    public virtual void Heal(int potency)
    {
        health.Heal(potency);
    }

    public virtual void ReceiveShield(int potency)
    {
        // Implement shield logic here
    }

    protected virtual void OnDie()
    {
        Debug.Log("Entity died");
        // gameObject.SetActive(false);
    }
}
