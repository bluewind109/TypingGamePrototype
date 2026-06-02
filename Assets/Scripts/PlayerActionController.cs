using UnityEngine;

public class PlayerActionController : MonoBehaviour, IActionController
{
    public System.Action onBasicActionExecuted;

    [SerializeField] private ActionConfig config;
    [SerializeField] private GameObject actionUI;

    public void OnActionSelected(CombatAction action, GameObject target)
    {
        // Give player 1 AP if the action is a basic attack or basic defend
        // if (action is Action_BasicAttack || action is Action_BasicDefend)
        // {
        //     onBasicActionExecuted?.Invoke();
        // }
        action.Execute();
    }
}
