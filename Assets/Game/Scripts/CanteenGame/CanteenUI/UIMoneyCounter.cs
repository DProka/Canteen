
using TMPro;
using UnityEngine;

public class UIMoneyCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundMoneyText;

    public void Init()
    {
        
    }

    public void UpdateRoundMoney(int roundMoney)
    {
        roundMoneyText.text = "Round Money: " + roundMoney;
    }

    public void ResetCounter()
    {
        roundMoneyText.text = "Round Money: 0";
    }
}
