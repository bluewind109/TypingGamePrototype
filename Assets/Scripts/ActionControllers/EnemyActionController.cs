using UnityEngine;

public class EnemyActionController : MonoBehaviour, IActionController
{
    [SerializeField] private ActionConfig config;

	public void OnActionSelected(CombatAction action)
	{
		throw new System.NotImplementedException();
	}
}
