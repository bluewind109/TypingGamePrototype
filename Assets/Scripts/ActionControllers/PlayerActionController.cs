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

    private List<CombatAction> actionQueue = new List<CombatAction>();

    private int currentAP = 0;
    private int maxAction = 6;

    void Awake()
    {
        player = GetComponent<Player>();
        sentenceManager.onActionTyped += OnActionTyped;

    }

    void Start()
    {
        sentenceManager.Reset();
    }

    void OnDestroy()
    {
        sentenceManager.onActionTyped -= OnActionTyped;

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
        // actionBar.AddAction(action);

        string actionNames = string.Join(", ", actionQueue.ConvertAll(a => a.name));
        // Debug.Log($"<color=blue>Current queue:</color> {actionNames}");

        if (actionQueue.Count >= maxAction)
        {
            // GameManager.Instance.StartCombat();
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

        onSkillUsed?.Invoke(typedAction.apCost);
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
    }

    private void OnSkillMenuBack()
    {
    }

    public void UpdateAP(int newAP)
    {
        currentAP = newAP;
    }
}
