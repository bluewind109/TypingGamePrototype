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

    public abstract void StartTurn();
    public abstract void StartCombat();
    public abstract void EndTurn();

    public virtual void ReceiveEffect(EffectInfo effectInfo)
    {
        switch (effectInfo.type)
        {
            case EffectType.Damage:
                TakeDamage(effectInfo.potency);
                break;
            case EffectType.Heal:
                Heal(effectInfo.potency);
                break;
            default:
                Debug.LogWarning("Unhandled effect type: " + effectInfo.type);
                break;
        }
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
