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
        Debug.Log("Parry effect triggered.");
    }
}
