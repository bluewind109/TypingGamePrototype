using System;
using UnityEngine;

[RequireComponent(typeof(PlayerActionController))]
public class Player : MonoBehaviour, IDamageable, IHealable, IShieldable
{
    public Action<int> onActionPointsChanged;

    [SerializeField] protected Stats _stats;
    [SerializeField] private Health _health;

    private PlayerActionController _actionController;

    private int _actionPoints = 0;
    public int ActionPoints => _actionPoints;

    public bool IsResolved { get; private set; } = false;

    private const int MAX_ACTION_POINTS = 3;

    void Awake()
    {
        if (_stats != null)
        {
            _health.Initialize(_stats.health);
            _health.onDie += OnDie;
        }

        _actionController = GetComponent<PlayerActionController>();
    }

    void OnDestroy()
    {
        
    }

    public void UpdateEntity()
    {
        _actionController.UpdateController();
    }

    private void OnDie()
    {
        Debug.Log("Player died");
        // gameObject.SetActive(false);
    }

	public void TakeDamage(int potency)
	{
		_health.TakeDamage(potency);
	}

	public void Heal(int potency)
	{
		_health.Heal(potency);
	}

	public void ReceiveShield(int potency)
	{
		// Implement shield logic here
	}
}
