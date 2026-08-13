using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    public Action onBasicActionExecuted;
    public Action<int> onSkillUsed;

    [Header("Components")]
    [SerializeField] private ActionConfig config;
    [SerializeField] private SentenceManager sentenceManager;

    private Player _player;
    private int _currentAP = 0;

    void Awake()
    {
        _player = GetComponent<Player>();
        sentenceManager.onActionTyped += OnActionTyped;
    }

    void Start()
    {
        sentenceManager.Initialize(config.GetAllSkills());
        sentenceManager.ToggleInput(true);
    }

    void OnDestroy()
    {
        sentenceManager.onActionTyped -= OnActionTyped;
    }

    public void UpdateController()
    {
        sentenceManager.UpdateGameplay();
    }

    private void OnActionTyped(CombatAction typedAction)
    {
        // Give player 1 AP if the action is a basic attack or basic defend
        if (typedAction.HasTag(ActionTag.Basic))
        {
            onBasicActionExecuted?.Invoke();
        }

        onSkillUsed?.Invoke(typedAction.apCost);
    }

    private void HandleActionButton(ActionButtonType type)
    {
        switch (type)
        {
            case ActionButtonType.BasicAttack:
                break;
            case ActionButtonType.BasicDefend:
                break;
            default:
                Debug.Log("<color=red>Unknown action</color> selected");
                break;
        }
    }

    public void UpdateAP(int newAP)
    {
        _currentAP = newAP;
    }
}
