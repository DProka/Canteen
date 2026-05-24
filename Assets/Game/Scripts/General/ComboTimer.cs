
using UnityEngine;

public class ComboTimer
{
    private int comboStreak;

    private float maxTime;
    private float timer;

    private bool isActive;

    public ComboTimer(float max)
    {
        maxTime = max;
        timer = 0;
        isActive = false;

        EventBus.OnComboAdded += AddCombo;
    }

    public void UpdateScript()
    {
        if (isActive)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                UIController.Instance.UpdateComboTimer(timer, maxTime);
            }
            else
            {
                comboStreak = 0;
                isActive = false;
                UIController.Instance.UpdateComboTimer(0, maxTime);
                UIController.Instance.UpdateComboStreak(comboStreak);
            }
        }
    }

    public void AddCombo()
    {
        comboStreak++;
        timer = maxTime;
        isActive = true;
        UIController.Instance.UpdateComboStreak(comboStreak);
    }

    public void ResetCombo()
    {
        comboStreak = 0;
        timer = 0;
        isActive = false;
        UIController.Instance.UpdateComboTimer(0, maxTime);
        UIController.Instance.UpdateComboStreak(comboStreak);
    }
}
