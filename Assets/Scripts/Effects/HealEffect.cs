using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Effects/HealEffect")]
public class HealEffect : Effect
{
    void Awake()
    {
        effectName = "Heal";
    }

    public override void ApplyEffect(Entity target, int potency)
    {
        if (target == null)
        {
            return;
        }

        target.Heal(potency);
    }
}
