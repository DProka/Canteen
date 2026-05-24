
public class PlayerParams
{
    public static PlayerParams Instance { get; private set; }

    public int totalMoneyCounter { get; private set; }
    public int roundMoneyCounter { get; private set; }

    public PlayerParams()
    {
        Instance = this;
        totalMoneyCounter = 0;
        roundMoneyCounter = 0;

        EventBus.OnTotalMoneyChanged += AddTotalMoney;
        EventBus.OnRoundMoneyChanged += AddRoundMoney;
    }

    public void AddTotalMoney(int amount)
    {
        totalMoneyCounter += amount;
        UIController.Instance.UpdateTotalMoney(totalMoneyCounter);
    }

    public void AddRoundMoney(int amount)
    {
        roundMoneyCounter += amount;
        UIController.Instance.UpdateRoundCounter(roundMoneyCounter);
    }

    public void ResetRoundMoney()
    {
        roundMoneyCounter = 0;
        UIController.Instance.UpdateRoundCounter(roundMoneyCounter);
    }

    public void ResetMoney()
    {
        totalMoneyCounter = 0;
        roundMoneyCounter = 0;
    }
}
