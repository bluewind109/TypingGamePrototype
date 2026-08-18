using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IHealable, IShieldable
{
    [SerializeField] protected Stats _stats;
    [SerializeField] private Health _health;
    [SerializeField] private PlayerActionController _actionController;
	[SerializeField] private Enemy _enemy;

    private int _actionPoints = 0;
    public int ActionPoints => _actionPoints;
	[SerializeField] private ActionPoint_UI _actionPointUI;

    public bool IsResolved { get; private set; } = false;

    private const int MAX_ACTION_POINTS = 3;

    void Awake()
    {
        if (_stats != null)
        {
            _health.Initialize(_stats.health);
            _health.onDie += OnDie;
        }
    }

	void Start()
	{
		if (_actionController != null)
		{
			_actionController.Init(this, _enemy);
		}
		_actionPointUI.UpdateUI(_actionPoints);
	}

    void OnDestroy()
    {
        _health.onDie -= OnDie;
    }

    public void UpdateEntity()
    {
        _actionController?.UpdateController();
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

	public void UpdateActionPoint(int amount)
	{
		_actionPoints = Mathf.Clamp(_actionPoints + amount, 0, MAX_ACTION_POINTS);
		_actionPointUI.UpdateUI(_actionPoints);
	}
}
