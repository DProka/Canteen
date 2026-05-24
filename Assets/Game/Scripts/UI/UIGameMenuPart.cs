
using TMPro;
using UnityEngine;

public class UIGameMenuPart : MonoBehaviour
{
    [SerializeField] GameObject mainObject;

    [SerializeField] TextMeshProUGUI moneyCounterText;

    [SerializeField] GameObject startGame;
    [SerializeField] GameObject resumeGame;

    public void Init()
    {
        SwitchMenuActive(true);

        UpdateTotalCounter(PlayerParams.Instance.totalMoneyCounter);

        EventBus.OnGamePaused += SwitchResumeButton;
        EventBus.OnGameResumed += SwitchStartButton;
    }

    public void SwitchMenuActive(bool isActive)
    {
        mainObject.SetActive(isActive);
    }

    public void UpdateTotalCounter(int count)
    {
        moneyCounterText.text = "Total Money: " + count;
    }

    private void SwitchStartButton()
    {
        startGame.SetActive(true);
        resumeGame.SetActive(false);
    }

    private void SwitchResumeButton()
    {
        startGame.SetActive(false);
        resumeGame.SetActive(true);
    }
}
