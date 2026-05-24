
using TMPro;
using UnityEngine;

public class UIComboTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI comboValue;
    [SerializeField] VisualTimer comboImage;

    public void UpdateCombo(int combo)
    {
        comboValue.text = "Combo: " + combo;
    }

    public void UpdateFullness(float currentTime, float maxTime)
    {
        comboImage.UpdateTimer(currentTime, maxTime);
    }
}
