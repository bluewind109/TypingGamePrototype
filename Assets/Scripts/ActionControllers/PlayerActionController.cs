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

    private Player _player;
    private int _currentAP = 0;

    void Awake()
    {
        _player = GetComponent<Player>();
        sentenceManager.onActionsTyped += OnActionsTyped;
    }

    void Start()
    {
        sentenceManager.Initialize(config.GetAllSkills());
        sentenceManager.ToggleInput(true);
    }

    void OnDestroy()
    {
        sentenceManager.onActionsTyped -= OnActionsTyped;
    }

    public void UpdateController()
    {
        sentenceManager.UpdateGameplay();
    }

    private void OnActionsTyped(List<CombatAction> typedActions)
    {

    }

    public void UpdateAP(int newAP)
    {
        _currentAP = newAP;
    }
}
