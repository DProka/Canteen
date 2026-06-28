
using UnityEngine;

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

    private const string saveKey = "generalSave";

    public PlayerParams(PlayerStartParams playerParams)
    {
        Instance = this;

        Load(playerParams);
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

    public void UpdateTotalMoneyAfterRound()
    {
        totalMoneyCounter += roundMoneyCounter;
        ResetRoundMoney();
        Save();
    }

    public void AddTotalMoney(int amount)
    {
        totalMoneyCounter += amount;
        Save();
    }

    public void AddRoundMoney(int amount)
    {
        roundMoneyCounter += amount;
    }

    public void ResetRoundMoney()
    {
        roundMoneyCounter = 0;
    }

    public void ResetMoney()
    {
        totalMoneyCounter = 0;
        roundMoneyCounter = 0;
    }

    #endregion

    #region Save Load

    public void ResetSave()
    {
        SaveData.GeneralData general = new SaveData.GeneralData();

        totalMoneyCounter = general._totalMoneyCounter;
        roundMoneyCounter = general._roundMoneyCounter;

        tapeCount = general._tapeCount;
        breadCount = general._breadCount;
        sauceCount = general._sauceCount;

        glassCount = general._glassCount;
        drinkCount = general._drinkCount;

        burnerCount = general._burnerCount;
        rawFoodCount = general._rawFoodCount;

        Save();
    }

    public void Save()
    {
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }

    public void Load(PlayerStartParams playerParams)
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            var data = SaveManager.Load<SaveData.GeneralData>(saveKey);

            totalMoneyCounter = data._totalMoneyCounter;
            roundMoneyCounter = data._roundMoneyCounter;

            tapeCount = data._tapeCount;
            breadCount = data._breadCount;
            sauceCount = data._sauceCount;

            glassCount = data._glassCount;
            drinkCount = data._drinkCount;

            burnerCount = data._burnerCount;
            rawFoodCount = data._rawFoodCount;
        }
        else
        {
            SetStartParams(playerParams);
        }
    }

    private SaveData.GeneralData GetSaveSnapshot()
    {
        SaveData.GeneralData data = new SaveData.GeneralData()
        {

            _totalMoneyCounter = totalMoneyCounter,
            _roundMoneyCounter = roundMoneyCounter,

            _tapeCount = tapeCount,
            _breadCount = breadCount,
            _sauceCount = sauceCount,

            _glassCount = glassCount,
            _drinkCount = drinkCount,

            _burnerCount = burnerCount,
            _rawFoodCount = rawFoodCount,

        };

        return data;
    }

    private void SetStartParams(PlayerStartParams playerParams)
    {
        totalMoneyCounter = playerParams.totalMoneyCounter;
        roundMoneyCounter = playerParams.roundMoneyCounter;

        tapeCount = playerParams.tapeCount;
        breadCount = playerParams.breadCount;
        sauceCount = playerParams.sauceCount;

        glassCount = playerParams.glassCount;
        drinkCount = playerParams.drinkCount;

        burnerCount = playerParams.burnerCount;
        rawFoodCount = playerParams.rawFoodCount;

        Save();
    }

    #endregion
}
