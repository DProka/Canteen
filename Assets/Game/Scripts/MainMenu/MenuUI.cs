
using TMPro;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalMoneyText;

    public void Init()
    {

    }

    public void UpdateMenuUI()
    {
        UpdateTotalMoneyText();
    }

    private void UpdateTotalMoneyText()
    {
        totalMoneyText.text = "Total Money: " + PlayerParams.Instance.totalMoneyCounter;
    }
}
