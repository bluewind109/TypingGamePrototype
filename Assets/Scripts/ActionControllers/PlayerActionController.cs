using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ActionConfig config;
    [SerializeField] private SentenceManager sentenceManager;
    
    [Header("References")]
    [SerializeField] private Enemy enemy;

    private Player _player;
    private int _currentAP = 0;

    void Awake()
    {
        _player = GetComponent<Player>();
        sentenceManager.onActionsTyped += OnActionsTyped;
    }

    void Start()
    {
        sentenceManager.Initialize(config.GetAllSkills());
        sentenceManager.ToggleInput(true);
    }

    void OnDestroy()
    {
        sentenceManager.onActionsTyped -= OnActionsTyped;
    }

    public void UpdateController()
    {
        sentenceManager.UpdateGameplay();
    }

    /// <summary>
    /// - Loop through each action in the typed actions list.
    /// - Check if the action is basic or skill.
    /// - If basic, execute the action and increase AP.
    /// - If skill, check if the player has enough AP to use it.
    /// - If enough AP, execute the skill and decrease AP.
    /// </summary>
    /// <param name="typedActions"></param>
    private void OnActionsTyped(List<CombatAction> typedActions)
    {
        if (typedActions == null || typedActions.Count == 0) return;

        foreach (CombatAction action in typedActions)
        {
            ExecuteAction(action);
        }
    }

    private void ExecuteAction(CombatAction action)
    {
        if (action == null) return;

        foreach (EffectInfo effect in action.effects)
        {
            GameObject target = GetTarget(effect.targetTeam);
			effect.Apply(target);
        }
    }

    private GameObject GetTarget(TargetTeam targetTeam)
    {
        switch (targetTeam)
        {
            case TargetTeam.Self:
                return gameObject;
            case TargetTeam.Ally:
                return null; // Implement ally targeting logic if needed
            case TargetTeam.Enemy:
                return enemy.gameObject;
            default:
                return null;
        }
    }

    public void UpdateAP(int newAP)
    {
        _currentAP = newAP;
    }
}
