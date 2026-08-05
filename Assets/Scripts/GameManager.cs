using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player _player;
    [SerializeField] private Enemy _currentEnemy;
    [SerializeField] private Timer _actionTimer;

    private InitState _initState;
    private GameplayState _gameplayState;
    private GameOverState _gameOverState;

    private GameState _currentGameState;

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
        _initState = new InitState(_player, _currentEnemy, _actionTimer);
        _gameplayState = new GameplayState(_player, _currentEnemy);
        _gameOverState = new GameOverState();

        ChangeGameState(_initState);
    }

    void Update()
    {
        _currentGameState?.Update();
    }

    private void ChangeGameState(GameState newGameState)
    {
        if (newGameState == null)
        {
            Debug.LogError("New game state is null!");
            return;
        }

        _currentGameState = newGameState;
        _currentGameState.Enter();
    }

    public void EnterGameplayState()
    {
        ChangeGameState(_gameplayState);
    }
}
