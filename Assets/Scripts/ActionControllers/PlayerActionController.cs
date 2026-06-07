using UnityEngine;

public class PlayerActionController : MonoBehaviour, IActionController
{
    public System.Action onBasicActionExecuted;

    [SerializeField] private ActionConfig config;
    [SerializeField] private SentenceManager sentenceManager;
    [SerializeField] private GameObject actionUI;

    void Awake()
    {
        sentenceManager.onActionTyped += OnActionTyped;
    }

    void OnDestroy()
    {
        sentenceManager.onActionTyped -= OnActionTyped;
    }

    // Selected from UI
    public void OnActionSelected(CombatAction action, GameObject target)
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
}
