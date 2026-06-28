namespace SaveData
{
    [System.Serializable]
    public class GeneralData
    {
        public int _totalMoneyCounter;
        public int _roundMoneyCounter;

        public int _tapeCount;
        public int _breadCount;
        public int _sauceCount;

        public int _glassCount;
        public int _drinkCount;

        public int _burnerCount;
        public int _rawFoodCount;

        public GeneralData()
        {
            _totalMoneyCounter = 0;
            _roundMoneyCounter = 0;

            _tapeCount = 0;
            _breadCount = 0;
            _sauceCount = 0;

            _glassCount = 0;
            _drinkCount = 0;

            _burnerCount = 0;
            _rawFoodCount = 0;
        }
    }
}
