using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Loading,
    Playing,
    LevelCompleted,
    LevelFailed,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private string[] levelScenes = { "SampleScene" };

    [SerializeField]
    private ScoreSettings scoreSettings;

    private Lander lander;

    public GameState CurrentState { get; private set; } = GameState.Loading;
    public int CurrentLevelIndex { get; private set; }

    public int TotalScore { get; private set; }

    public int LevelScore { get; private set; }

    public int Score => TotalScore + LevelScore;
    public bool HasNextLevel => CurrentLevelIndex + 1 < levelScenes.Length;

    public event Action<GameState> OnStateChanged;
    public event Action<int> OnScoreChanged;
    public event Action<LandingResult> OnLevelCompleted;
    public event Action OnLevelFailed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        int index = Array.IndexOf(levelScenes, SceneManager.GetActiveScene().name);
        CurrentLevelIndex = Mathf.Max(index, 0);
    }

    private void Start()
    {
        InitLevel();
        SceneManager.sceneLoaded += SceneManager_OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance != this)
        {
            return;
        }

        SceneManager.sceneLoaded -= SceneManager_OnSceneLoaded;
        UnsubscribeFromLander();
        Instance = null;
    }

    public void LoadLevel(int index)
    {
        if (index < 0 || index >= levelScenes.Length)
        {
            Debug.LogError($"Level index {index} is out of range");
            return;
        }

        UnsubscribeFromLander();
        CurrentLevelIndex = index;
        SetState(GameState.Loading);
        SceneManager.LoadScene(levelScenes[index]);
    }

    public void RestartLevel()
    {
        SetLevelScore(0);
        LoadLevel(CurrentLevelIndex);
    }

    public void LoadNextLevel()
    {
        if (!HasNextLevel)
        {
            Debug.LogWarning("No next level");
            return;
        }

        TotalScore += LevelScore;
        SetLevelScore(0);
        LoadLevel(CurrentLevelIndex + 1);
    }

    [ContextMenu("Restart Level")]
    private void DebugRestartLevel() => RestartLevel();

    [ContextMenu("Load Next Level")]
    private void DebugLoadNextLevel() => LoadNextLevel();

    private void SceneManager_OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitLevel();
    }

    private void InitLevel()
    {
        LevelLoader levelLoader = FindAnyObjectByType<LevelLoader>();

        if (levelLoader == null)
        {
            Debug.LogError("LevelLoader not found in scene");
            return;
        }

        levelLoader.Load();
        SubscribeToLander(levelLoader.Lander);
        SetState(GameState.Playing);
    }

    private void FinishLevel(GameState finalState)
    {
        SetState(finalState);

        if (lander != null)
        {
            lander.enabled = false;
        }
    }

    private void SetState(GameState state)
    {
        CurrentState = state;
        Debug.Log($"Game state: {state}");
        OnStateChanged?.Invoke(state);
    }

    private void SetLevelScore(int value)
    {
        LevelScore = value;
        OnScoreChanged?.Invoke(Score);
    }

    private void SubscribeToLander(Lander newLander)
    {
        UnsubscribeFromLander();
        lander = newLander;

        if (lander == null)
        {
            Debug.LogError("Lander is not assigned in LevelLoader");
            return;
        }

        lander.OnCoinPicked += Lander_OnCoinPicked;
        lander.OnLanded += Lander_OnLanded;
        lander.OnCrashed += Lander_OnCrashed;
    }

    private void UnsubscribeFromLander()
    {
        if (lander == null)
        {
            return;
        }

        lander.OnCoinPicked -= Lander_OnCoinPicked;
        lander.OnLanded -= Lander_OnLanded;
        lander.OnCrashed -= Lander_OnCrashed;
        lander = null;
    }

    private void Lander_OnCoinPicked(float value)
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }

        SetLevelScore(LevelScore + Mathf.RoundToInt(value));
        Debug.Log($"Coin picked: +{value}, score: {Score}");
    }

    private void Lander_OnLanded(LandingData landing)
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }

        LandingResult result = ScoreCalculator.Calculate(landing, lander.Settings, scoreSettings);

        if (!result.success)
        {
            Debug.Log(
                $"Bad landing: velocity {landing.velocity:F2}, angle dot {landing.angleDot:F2}"
            );
            Fail();
            return;
        }

        SetLevelScore(LevelScore + result.total);
        Debug.Log(
            $"Landed: ({result.angleScore} angle + {result.speedScore} speed) x{result.multiplier} = {result.total}, score: {Score}"
        );
        FinishLevel(GameState.LevelCompleted);
        OnLevelCompleted?.Invoke(result);
    }

    private void Lander_OnCrashed()
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }

        Fail();
    }

    private void Fail()
    {
        FinishLevel(GameState.LevelFailed);
        OnLevelFailed?.Invoke();
    }
}
