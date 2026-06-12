using UnityEngine;

public class StatusEffect
{
    public System.Action<StatusEffectType> onStatusEffectTimedOut;

    public StatusEffectType effectType;
    public int currentStacks;
    public int maxStacks;
    public float timer = 0f;

    public void Tick(float deltaTime)
    {
        if (timer > 0f)
        {
            timer -= deltaTime;
            if (timer <= 0f)
            {
                onStatusEffectTimedOut?.Invoke(effectType);
            }
        }
    }
}
