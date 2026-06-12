using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    public static StatusEffectManager Instance { get; private set; }

    [SerializeField] private StatusEffectConfig statusEffectConfig;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public StatusEffectInfo GetStatusEffectInfo(StatusEffectType effectType)
    {
        return statusEffectConfig.GetStatusEffectInfo(effectType);
    }
}
