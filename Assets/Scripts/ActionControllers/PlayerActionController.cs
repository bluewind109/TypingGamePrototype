using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour, IActionController
{
    public Action onBasicActionExecuted;
    public Action<int> onSkillUsed;

    [SerializeField] private Enemy enemy;
    [Header("Components")]
    [SerializeField] private ActionConfig config;
    [SerializeField] private SentenceManager sentenceManager;
    private EffectHandler effectHandler;
    private Player player;
    [Header("Action menu UI")]
    [SerializeField] private ActionMenu actionMenu;
    [SerializeField] private SkillMenu skillMenu;
    [SerializeField] private TMPro.TextMeshProUGUI menuTitleText;

    [Header("Action Queue UI")]
    [SerializeField] private PlayerActionBar actionBar;

    private List<CombatAction> actionQueue = new List<CombatAction>();

    private int currentAP = 0;
    private int maxAction = 6;

    void Awake()
    {
        effectHandler = GetComponent<EffectHandler>();
        player = GetComponent<Player>();
        sentenceManager.onActionTyped += OnActionTyped;
        actionMenu.onMenuOpened += () => menuTitleText.text = "Actions";
        actionMenu.onActionButtonPressed += HandleActionButton;
        actionMenu.onSkillPressed += OnActionSkillPressed;
        skillMenu.onMenuOpened += () => menuTitleText.text = "Skills";
        skillMenu.onActionButtonPressed += OnActionSelected;
        skillMenu.onBackPressed += OnSkillMenuBack;
    }

    void Start()
    {
        sentenceManager.Reset();
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
        skillMenu.onMenuOpened -= () => menuTitleText.text = "Skills";
        actionMenu.onMenuOpened -= () => menuTitleText.text = "Actions";
    }

    public void StartTurn()
    {
        actionMenu.ShowMenu();
        skillMenu.HideMenu();
        sentenceManager.ToggleInput(false);
    }

    public void StartCombat()
    {
        actionMenu.HideMenu();
        skillMenu.HideMenu();
        sentenceManager.ToggleInput(true);
        GetNextAction();
    }

    public void EndTurn()
    {
        actionBar.ClearActionBar();
        actionMenu.HideMenu();
        skillMenu.HideMenu();
        sentenceManager.ToggleInput(false);
    }

    // Selected from UI
    public void OnActionSelected(CombatAction action)
    {
        if (sentenceManager.HasActiveSentence)
        {
            Debug.Log("<color=red>Cannot select new action while another action is active.</color>");
            return;
        }

        if (currentAP < action.apCost)
        {
            Debug.Log("<color=red>Not enough AP to use this skill.</color>");
            return;
        }

        menuTitleText.text = string.Empty;
        AddActionToQueue(action);
    }

    private void AddActionToQueue(CombatAction action)
    {
        if (actionQueue.Count >= maxAction) return;

        actionQueue.Add(action);
        actionBar.AddAction(action);

        string actionNames = string.Join(", ", actionQueue.ConvertAll(a => a.name));
        // Debug.Log($"<color=blue>Current queue:</color> {actionNames}");

        if (actionQueue.Count >= maxAction)
        {
            GameManager.Instance.StartCombat();
        }
    }

    void GetNextAction()
    {
        if (actionQueue.Count == 0) return;

        CombatAction nextAction = actionQueue[0];
        actionQueue.RemoveAt(0);
        sentenceManager.LoadAction(nextAction);
    }

    private void OnActionTyped(CombatAction typedAction)
    {
        // Give player 1 AP if the action is a basic attack or basic defend
        if (typedAction.HasTag(ActionTag.Basic))
        {
            onBasicActionExecuted?.Invoke();
        }

        effectHandler.HandleAction(typedAction, player, enemy);
        onSkillUsed?.Invoke(typedAction.apCost);
        actionBar.OnActionExecuted();
        GetNextAction();
    }

    private void HandleActionButton(ActionButtonType type)
    {
        switch (type)
        {
            case ActionButtonType.BasicAttack:
                AddActionToQueue(config.basicAttack);
                break;
            case ActionButtonType.BasicDefend:
                AddActionToQueue(config.basicDefend);
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

    public void UpdateAP(int newAP)
    {
        currentAP = newAP;
    }
}
