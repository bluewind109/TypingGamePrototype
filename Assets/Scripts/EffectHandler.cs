using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    private Entity source;
    private Entity target;

    public void Initialize(Entity source, Entity target)
    {
        this.source = source;
        this.target = target;
    }

    public void HandleEffect(Effect effect)
    {
        effect.ApplyEffect();
    }
}
