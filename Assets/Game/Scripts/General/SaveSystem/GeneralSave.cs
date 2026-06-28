
public class GeneralSave
{
    public static GeneralSave Instance;

    public int totalMoneyCounter;
    public int roundMoneyCounter;

    public int tapeCount;
    public int breadCount;
    public int sauceCount;

    public int glassCount;
    public int drinkCount;

    public int burnerCount;
    public int rawFoodCount;

    private const string saveKey = "generalSave";

    public GeneralSave()
    {
        Instance = this;

        totalMoneyCounter = 0;
        roundMoneyCounter = 0;

        tapeCount = 0;
        breadCount = 0;
        sauceCount = 0;

        glassCount = 0;
        drinkCount = 0;

        burnerCount = 0;
        rawFoodCount = 0;

        Load();
    }

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

    public void Load()
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

    #endregion
}
