using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Enemy currentEnemy;

    private GameState currentState = GameState.None;

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
        StartTurn();
    }

    void SetState(GameState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
    }

    public void StartTurn()
    {
        SetState(GameState.TurnStart);
    }

    public void StartCombat()
    {
        SetState(GameState.Combat);
    }

    public void EndTurn()
    {
        SetState(GameState.TurnEnd);
    }
}

public enum GameState
{
    None = -1,
    TurnStart,
    Combat,
    TurnEnd,
    Victory,
}
