using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Effects/HealEffect")]
public class HealEffect : Effect
{
    void Awake()
    {
        effectName = "Heal";
    }

    public override void ApplyEffect()
    {
        // Implement heal application logic here
        
    }
}
