using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] private ActionConfig config;

    public void ExecuteAction(CombatAction action, GameObject target)
    {
        action.Execute(target);
    }
}
