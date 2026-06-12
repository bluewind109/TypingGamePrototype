using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectConfig", menuName = "Configs/StatusEffectConfig")]
public class StatusEffectConfig : ScriptableObject
{
    public List<StatusEffectInfo> statusEffects = new List<StatusEffectInfo>();

    public StatusEffectInfo GetStatusEffectInfo(StatusEffectType effectType)
    {
        foreach (var info in statusEffects)
        {
            if (info.effectType == effectType)
            {
                return info;
            }
        }
        return null;
    }
}
