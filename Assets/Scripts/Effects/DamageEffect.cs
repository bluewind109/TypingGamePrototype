using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Effects/DamageEffect")]
public class DamageEffect : Effect
{
    void Awake()
    {
        effectName = "Damage";
    }

    public override void ApplyEffect(Entity target, int potency)
    {
        if (target == null)
        {
            return;
        }

        target.TakeDamage(potency);
    }
}
