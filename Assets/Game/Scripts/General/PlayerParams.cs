
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
    public int rawFoodCount { get; private set; }

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
        rawFoodCount = 1;
    }

    #region StaffParams

    public void UpdateStaffParams(StaffType staffType, int count)
    {
        switch (staffType)
        {
            case StaffType.Tape: tapeCount += count; break;
            case StaffType.Bread: breadCount += count; break;
            case StaffType.Sauce: sauceCount += count; break;
            case StaffType.Glass: glassCount += count; break;
            case StaffType.Drink: drinkCount += count; break;
            case StaffType.Burner: burnerCount += count; break;
            case StaffType.RawFood: rawFoodCount += count; break;
        }
    }

    public int GetCurrentStaffCountByType(StaffType type)
    {
        switch (type)
        {
            case StaffType.Tape: return tapeCount;
            case StaffType.Bread: return breadCount;
            case StaffType.Sauce: return sauceCount;
            case StaffType.Burner: return burnerCount;
            case StaffType.RawFood: return rawFoodCount;
            case StaffType.Glass: return glassCount;
            default: return 0;
        }
    }

    public int GetMaxStaffCountByType(StaffType type)
    {
        switch (type)
        {
            case StaffType.Tape: return 4;
            case StaffType.Bread: return 2;
            case StaffType.Sauce: return 2;
            case StaffType.Burner: return 4;
            case StaffType.RawFood: return 2;
            case StaffType.Glass: return 3;
            default: return 0;
        }
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
