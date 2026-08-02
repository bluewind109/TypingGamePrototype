using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Enemy currentEnemy;

    private GamePhase skillSelectionPhase;
    private GamePhase combatPhase;
    private GamePhase currentPhase = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        skillSelectionPhase = new SkillSelectionPhase(player, currentEnemy);
        combatPhase = new CombatPhase(player, currentEnemy);
        SetPhase(skillSelectionPhase);
    }

    void Update()
    {
        if (currentPhase != null)
        {
            currentPhase.Update();
        }
    }

    void SetPhase(GamePhase newPhase)
    {
        if (newPhase == null) return;
        if (newPhase == currentPhase) return;

        currentPhase = newPhase;
        currentPhase.Begin();
    }
}
