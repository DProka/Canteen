
using UnityEngine;
using UnityEngine.UI;

public class VisualTimer : MonoBehaviour
{
    [SerializeField] GameObject timerObj;
    [SerializeField] Image timerVisualization;

    public void SwitchTimerObject(bool active)
    {
        timerObj.SetActive(active);
    }

    public void UpdateTimer(float currentTime, float maxTime)
    {
        float fullness = currentTime / maxTime;

        fullness = Mathf.Clamp01(fullness);

        timerVisualization.fillAmount = fullness;
    }
}
