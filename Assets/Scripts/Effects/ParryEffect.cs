using UnityEngine;

[CreateAssetMenu(fileName = "ParryEffect", menuName = "Effects/ParryEffect")]
public class ParryEffect : Effect
{
    void Awake()
    {
        effectName = "Parry";
    }

    public override void ApplyEffect()
    {
        // Implement parry application logic here
    }
}
