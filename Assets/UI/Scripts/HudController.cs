using CosmicLander.UI;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HudController : MonoBehaviour
{
    [SerializeField]
    private Lander lander;

    [SerializeField]
    private ScoreKeeper scoreKeeper;

    private ScoreLabel scoreLabel;
    private IconButton menuButton,
        restartButton;

    private FuelBar fuelBar;
    private SpeedGauge speedGauge;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        fuelBar = root.Q<FuelBar>("fuel-bar");
        speedGauge = root.Q<SpeedGauge>("speed-gauge");
        scoreLabel = root.Q<ScoreLabel>("score-label");
        menuButton = root.Q<IconButton>("menu-button");
        restartButton = root.Q<IconButton>("restart-button");

        lander.OnFuelChanged += HandleFuelChanged;
        scoreKeeper.OnScoreChanged += scoreLabel.SetValue;
        menuButton.Clicked += OnMenuClicked;
        restartButton.Clicked += OnRestartClicked;
    }

    private void OnDisable()
    {
        lander.OnFuelChanged -= HandleFuelChanged;
        scoreKeeper.OnScoreChanged -= scoreLabel.SetValue;
        menuButton.Clicked -= OnMenuClicked;
        restartButton.Clicked -= OnRestartClicked;
    }

    private void Start()
    {
        float safe = lander.SafeSpeedKmh;
        speedGauge.Configure(60f, safe, safe * 1.5f);
        fuelBar.SetValue(lander.FuelNormalized);
        scoreLabel.SetValue(scoreKeeper.Score);
    }

    private void Update()
    {
        speedGauge.SetSpeed(lander.SpeedKmh);
    }

    private void HandleFuelChanged(object sender, System.EventArgs e)
    {
        fuelBar.SetValue(lander.FuelNormalized);
    }

    private void OnMenuClicked() => Debug.Log("Menu clicked");

    private void OnRestartClicked() => Debug.Log("Restart clicked");
}
