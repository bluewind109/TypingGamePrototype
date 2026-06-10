using UnityEngine;

[CreateAssetMenu(fileName = "ParryEffect", menuName = "Effects/ParryEffect")]
public class ParryEffect : Effect
{
    void Awake()
    {
        effectName = "Parry";
    }

    public override void ApplyEffect(Entity target, int potency)
    {
        // TODO add parry buff to target, which blocks the next incoming damage and then removes itself
    }
}
