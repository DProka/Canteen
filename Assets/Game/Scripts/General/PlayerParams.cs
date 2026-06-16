
public class PlayerParams
{
    public static PlayerParams Instance { get; private set; }

    public int totalMoneyCounter { get; private set; }
    public int roundMoneyCounter { get; private set; }

    public int tapeCount { get; private set; }
    public int breadCount { get; private set; }
    public int sauceCount { get; private set; }

    public int glassCount { get; private set; }
    public int drinkCount { get; private set; }

    public int burnerCount { get; private set; }
    public int rawMeatCount { get; private set; }

    public PlayerParams()
    {
        Instance = this;

        SetStartParams();
        
        EventBus.OnTotalMoneyChanged += AddTotalMoney;
        EventBus.OnRoundMoneyChanged += AddRoundMoney;
    }

    private void SetStartParams()
    {
        totalMoneyCounter = 0;
        roundMoneyCounter = 0;

        tapeCount = 1;
        breadCount = 1;
        sauceCount = 0;

        glassCount = 1;
        drinkCount = 1;

        burnerCount = 1;
        rawMeatCount = 1;
    }

    #region Delivery Area

    public void UpdateTapeCount(int count)
    {
        tapeCount += count;
    }

    public void UpdateBreadCount(int count)
    {
        tapeCount += count;
    }

    public void UpdateSauceCount(int count)
    {
        tapeCount += count;
    }

    #endregion

    #region Bar Area

    public void UpdateGlassCount(int count)
    {
        glassCount += count;
    }

    public void UpdateBarrelCount(int count)
    {
        drinkCount += count;
    }

    #endregion

    #region Kitchen Area

    public void UpdateBurnersCount(int count)
    {
        burnerCount += count;
    }

    public void UpdateRawMeatCount(int count)
    {
        rawMeatCount += count;
    }

    #endregion

    #region Money

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

    #endregion
}
