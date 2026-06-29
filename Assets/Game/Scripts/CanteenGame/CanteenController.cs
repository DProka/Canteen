
using UnityEngine;

public class CanteenController : MonoBehaviour
{
    [Header("General")]

    [SerializeField] SoundController soundController;

    [Header("Game")]

    [SerializeField] KitchenController kitchenController;
    [SerializeField] OrderManager orderManager;

    [Header("UI")]

    [SerializeField] UIController uiController;

    [Header("Settings")]

    [SerializeField] KitchenSettings kitchenSettings;

    //private ClickManager clickManager;

    private RoundTimer roundTimer;
    private ComboTimer comboTimer;

    private bool gameIsActive;

    private void Awake()
    {
        //GameController.CreateIfNeeded();

        //clickManager = new ClickManager();

        roundTimer = new RoundTimer(60f);
        comboTimer = new ComboTimer(10f);
    }

    private void Start()
    {
        roundTimer = new RoundTimer(60f);
        comboTimer = new ComboTimer(10f);

        EventBus.OnRoundEnded += EndRound;

        EventBus.OnGamePaused += PauseGame;
        EventBus.OnGameResumed += ResumeGame;

        EventBus.OnRoundMoneyChanged += AddRoundMoney;

        soundController.Init();
        kitchenController.Init(kitchenSettings);
        orderManager.Init(kitchenSettings);
        uiController.Init(this);

        gameIsActive = false;
    }

    private void Update()
    {
        if (!gameIsActive)
            return;

        //clickManager.UpdateScript();
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
        UpdateTotalMoney();

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

    #region Reward

    public void UpdateTotalMoney()
    {
        GameController.Instance.UpdateTotalMoneyAfterRound();
        UIController.Instance.UpdateTotalMoney();
    }

    private void AddRoundMoney(int amount)
    {
        GameController.Instance.AddRoundMoney(amount);
        UIController.Instance.UpdateRoundCounter();
    }

    public void ResetRoundMoney()
    {
        PlayerParams.Instance.ResetRoundMoney();
        UIController.Instance.UpdateRoundCounter();
    }

    #endregion

    private void OnDestroy()
    {
        EventBus.OnRoundEnded -= EndRound;

        EventBus.OnGamePaused -= PauseGame;
        EventBus.OnGameResumed -= ResumeGame;

        EventBus.OnRoundMoneyChanged -= AddRoundMoney;
    }
}
