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

    void Update()
    {
        switch (currentState)
        {
            case GameState.TurnStart:
                // Handle any turn start logic if needed
                break;
            case GameState.Combat:
                // Handle combat logic if needed
                break;
            case GameState.TurnEnd:
                // Handle any turn end logic if needed
                if (player.IsResolved && currentEnemy.IsResolved)
                {
                    StartTurn();
                }
                break;
            case GameState.Victory:
                // Handle victory logic if needed
                break;
        }
    }

    void SetState(GameState newState)
    {
        if (currentState == newState) return;
        currentState = newState;

        switch (currentState)
        {
            case GameState.TurnStart:
                player.StartTurn();
                currentEnemy.StartTurn();
                break;
            case GameState.Combat:
                player.StartCombat();
                currentEnemy.StartCombat();
                break;
            case GameState.TurnEnd:
                player.EndTurn();
                currentEnemy.EndTurn();
                break;
            case GameState.Victory:
                // Handle victory state
                break;
        }
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
