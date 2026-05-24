
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] KitchenController kitchenController;
    [SerializeField] OrderManager orderManager;

    [Header("UI")]

    [SerializeField] UIController uiController;

    [Header("Settings")]

    [SerializeField] KitchenSettings kitchenSettings;

    private ClickManager clickManager;
    private PlayerParams playerParams;

    private RoundTimer roundTimer;
    private ComboTimer comboTimer;

    private bool gameIsActive;

    private void Awake()
    {
        clickManager = new ClickManager();
        playerParams = new PlayerParams();

        roundTimer = new RoundTimer(60f);
        comboTimer = new ComboTimer(10f);
    }

    private void Start()
    {
        kitchenController.Init(kitchenSettings);
        orderManager.Init(kitchenSettings);
        uiController.Init(this);

        gameIsActive = false;

        EventBus.OnRoundEnded += EndRound;

        EventBus.OnGamePaused += PauseGame;
        EventBus.OnGameResumed += ResumeGame;
    }

    private void Update()
    {
        if (!gameIsActive)
            return;

        clickManager.UpdateScript();
        kitchenController.UpdateScript();
        orderManager.UpdateScript();

        comboTimer.UpdateScript();
        roundTimer.UpdateTimer();
    }

    #region Round

    public void StartRound()
    {
        kitchenController.ResetPart();
        orderManager.ResetPart();
        SwitchGameActive(true);
        roundTimer.StartTimer();
        comboTimer.ResetCombo();

        EventBus.OnRoundStarted?.Invoke();
    }

    public void EndRound()
    {
        playerParams.AddTotalMoney(playerParams.roundMoneyCounter);
        playerParams.ResetRoundMoney();

        kitchenController.ResetPart();
        orderManager.ResetPart();
        SwitchGameActive(false);
        uiController.EndRound();
    }

    public void ResetRound()
    {
        kitchenController.ResetPart();
        orderManager.ResetPart();
        SwitchGameActive(true);
    }

    public void SwitchGameActive(bool active) => gameIsActive = active;

    private void PauseGame() => SwitchGameActive(false);

    private void ResumeGame() => SwitchGameActive(true);

    #endregion
}
