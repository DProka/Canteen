
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] UIRoundPart roundPart;
    [SerializeField] UIGameMenuPart gameMenuPart;

    private GameController gameController;

    public void Init(GameController controller)
    {
        Instance = this;
        gameController = controller;

        roundPart.Init();
        gameMenuPart.Init();

        roundPart.SwitchPartActive(false);
        gameMenuPart.SwitchMenuActive(true);
    }

    #region Game Part

    public void StartRound()
    {
        gameController.StartRound();
        roundPart.ResetPart();
        roundPart.SwitchPartActive(true);
        gameMenuPart.SwitchMenuActive(false);
    }

    public void PauseGame()
    {
        gameMenuPart.SwitchMenuActive(true);
        EventBus.OnGamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        gameMenuPart.SwitchMenuActive(false);
        EventBus.OnGameResumed?.Invoke();
    }

    public void EndRound()
    {
        roundPart.SwitchPartActive(false);
        gameMenuPart.SwitchMenuActive(true);
    }

    public void ResetRound()
    {
        StartRound();
        //gameController.ResetRound();
        //roundPart.SwitchPartActive(false);
        //gameMenuPart.SwitchMenuActive(true);
    }

    #endregion

    #region Round Part

    public void UpdateRoundTimer(int time) => roundPart.UpdateRoundTimer(time);

    public void UpdateComboStreak(int streak) => roundPart.UpdateComboStreak(streak);

    public void UpdateComboTimer(float time, float max) => roundPart.UpdateComboTimer(time, max);

    public void UpdateRoundCounter(int count) => roundPart.UpdateRoundCounter(count);

    #endregion

    #region Menu Part

    public void UpdateTotalMoney(int count)
    {
        gameMenuPart.UpdateTotalCounter(count);
    }

    #endregion
}
