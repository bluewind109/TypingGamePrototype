using UnityEngine;

public interface IActionController
{
    void OnActionSelected(CombatAction action, GameObject target);
}
