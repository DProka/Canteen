
using TMPro;
using UnityEngine;

public class UIRoundTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    public void UpdateTime(int time)
    {
        timerText.text = "Remaining time: " + time;
    }
}
