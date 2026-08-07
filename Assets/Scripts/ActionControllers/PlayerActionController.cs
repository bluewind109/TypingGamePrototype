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
    private Player player;
    [Header("Action menu UI")]
    [SerializeField] private TMPro.TextMeshProUGUI menuTitleText;

    private int currentAP = 0;
    private int maxAction = 6;

    void Awake()
    {
        player = GetComponent<Player>();
        sentenceManager.onActionTyped += OnActionTyped;
    }

    void Start()
    {
        sentenceManager.Initialize(config.GetAllSkills());
    }

    void OnDestroy()
    {
        sentenceManager.onActionTyped -= OnActionTyped;

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
        currentAP = newAP;
    }
}
