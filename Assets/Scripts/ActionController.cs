using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] private ActionConfig config;
    [SerializeField] private GameObject actionUI;

    private void OnActionSelected(CombatAction action, GameObject target)
    {
        action.Execute(target);
    }
}
