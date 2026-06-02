using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Effects/DamageEffect")]
public class DamageEffect : Effect
{
    void Awake()
    {
        effectName = "Damage";
    }

    public override void ApplyEffect()
    {
        // Implement damage application logic here
        
    }
}
