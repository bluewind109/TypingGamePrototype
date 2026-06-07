using System;
using UnityEngine;

public class PlayerActionController : MonoBehaviour, IActionController
{
    public Action onBasicActionExecuted;

    [Header("Components")]
    [SerializeField] private ActionConfig config;
    [SerializeField] private SentenceManager sentenceManager;

    [Header("UI References")]
    [SerializeField] private ActionMenu actionMenu;
    [SerializeField] private SkillMenu skillMenu;

    void Awake()
    {
        sentenceManager.onActionTyped += OnActionTyped;
        actionMenu.onActionButtonPressed += HandleActionButton;
        actionMenu.onSkillPressed += OnActionSkillPressed;
        skillMenu.onActionButtonPressed += OnActionSelected;
        skillMenu.onBackPressed += OnSkillMenuBack;
    }

    void Start()
    {
        actionMenu.ShowMenu();
        _ = skillMenu.Init(config.skills);
    }

    void OnDestroy()
    {
        sentenceManager.onActionTyped -= OnActionTyped;
        actionMenu.onActionButtonPressed -= HandleActionButton;
        actionMenu.onSkillPressed -= OnActionSkillPressed;
        skillMenu.onActionButtonPressed -= OnActionSelected;
        skillMenu.onBackPressed -= OnSkillMenuBack;
    }

    // Selected from UI
    public void OnActionSelected(CombatAction action)
    {
        sentenceManager.LoadAction(action);
    }

    private void OnActionTyped(CombatAction typedAction)
    {
        // Give player 1 AP if the action is a basic attack or basic defend
        if (typedAction.HasTag(ActionTag.Basic))
        {
            onBasicActionExecuted?.Invoke();
        }

        typedAction.Execute();
    }

    private void HandleActionButton(ActionButtonType type)
    {
        switch (type)
        {
            case ActionButtonType.BasicAttack:
                Debug.Log("<color=yellow>Attack</color> selected");
                break;
            case ActionButtonType.BasicDefend:
                Debug.Log("<color=yellow>Defend</color> selected");
                break;
            default:
                Debug.Log("<color=red>Unknown action</color> selected");
                break;
        }
    }

    private void OnActionSkillPressed()
    {
        actionMenu.HideMenu();
        skillMenu.ShowMenu();
    }

    private void OnSkillMenuBack()
    {
        skillMenu.HideMenu();
        actionMenu.ShowMenu();
    }
}
