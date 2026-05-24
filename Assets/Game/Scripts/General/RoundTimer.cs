
using UnityEngine;

public class RoundTimer
{
    private float maxTime;
    private float timer;

    private bool isActive;

    public RoundTimer(float max)
    {
        maxTime = max;
        timer = maxTime;
    }

    public void UpdateTimer()
    {
        if (isActive)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                UIController.Instance.UpdateRoundTimer((int)timer);
            }
            else
            {
                isActive = false;
                EventBus.OnRoundEnded?.Invoke();
            }
        }
    }

    public void StartTimer()
    {
        timer = maxTime;
        isActive = true;
        UIController.Instance.UpdateRoundTimer((int)timer);
    }
}
