
using UnityEngine;

public class UIRoundPart : MonoBehaviour
{
    [SerializeField] UIRoundTimer roundTimer;
    [SerializeField] UIComboTimer comboTimer;
    [SerializeField] UIMoneyCounter moneyCounter;

    public void Init()
    {
    }

    public void SwitchPartActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void UpdateRoundTimer(int time)
    {
        roundTimer.UpdateTime(time);
    }

    public void UpdateComboStreak(int streak)
    {
        comboTimer.UpdateCombo(streak);
    }

    public void UpdateComboTimer(float time, float max)
    {
        comboTimer.UpdateFullness(time, max);
    }

    public void UpdateRoundCounter(int count)
    {
        moneyCounter.UpdateRoundMoney(count);
    }

    public void ResetPart()
    {
        comboTimer.UpdateCombo(0);
        comboTimer.UpdateFullness(0, 10);
        moneyCounter.ResetCounter();
    }
}
