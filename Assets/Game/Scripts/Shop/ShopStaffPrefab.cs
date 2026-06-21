
using TMPro;
using UnityEngine;

public class ShopStaffPrefab : MonoBehaviour, IClickable
{
    [SerializeField] int staffID;
    [SerializeField] int price;
    [SerializeField] SpriteRenderer[] spritesArray;

    [SerializeField] TextMeshProUGUI priceText;

    private StaffType type;

    public void Init(int _id, StaffType _type, int _price)
    {
        staffID = _id;
        type = _type;
        price = _price;

        UpdatePriceText();
    }

    public void OnClick()
    {
        if (CheckPrice() && CheckMaxStaffCount())
        {
            BuyStaff();
        }
    }

    private void BuyStaff()
    {
        PlayerParams.Instance.AddRoundMoney(-price);
        EventBus.OnStaffBuyed?.Invoke(type);
        UpdateSpritesVisibility();
    }

    private bool CheckPrice()
    {
        return price <= PlayerParams.Instance.totalMoneyCounter;
    }

    private bool CheckMaxStaffCount()
    {
        int currentCount = PlayerParams.Instance.GetCurrentStaffCountByType(type);
        int maxCount = PlayerParams.Instance.GetMaxStaffCountByType(type);
        return currentCount < maxCount;
    }

    private void UpdateSpritesVisibility()
    {
        int currentCount = PlayerParams.Instance.GetCurrentStaffCountByType(type) - 1;

        for (int i = 0; i < spritesArray.Length; i++)
        {
            if(i < currentCount)
                spritesArray[i].enabled = false;
            else
                spritesArray[i].enabled = true;
        }
    }

    private void UpdatePriceText()
    {
        priceText.text = price + "P";
    }
}
