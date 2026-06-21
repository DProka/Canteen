
using System;

public class EventBus
{
    #region General Events

    public static Action OnRoundStarted;
    public static Action OnRoundEnded;

    public static Action OnGamePaused;
    public static Action OnGameResumed;

    #endregion

    #region Orders Events

    public static Action<int> OnNpcStoped;
    public static Action<int> OnOrderDelete;

    #endregion

    #region Delivery Events

    public static Action<int> OnBreadClicked;
    public static Action<int> OnSauceClicked;
    public static Action<int> OnReadyFoodClicked;
    public static Action<int> OnReadyFoodRemoved;

    public static Action OnTrashCanClicked;

    #endregion

    #region Cooking Events

    public static Action<int> OnRawFoodClicked;
    public static Action<int, int> OnCookedFoodClicked;
    public static Action<int> OnBurnerReseted;

    #endregion

    #region Bar Events

    public static Action OnGlassBoxClicked;
    public static Action<int> OnCanClicked;
    public static Action<int, int> OnFullGlassClicked;

    #endregion

    #region Combo Events

    public static Action OnComboAdded;

    #endregion

    #region Player Params Events

    public static Action<int> OnTotalMoneyChanged;
    public static Action<int> OnRoundMoneyChanged;

    #endregion

    #region Shop Events

    public static Action<StaffType> OnStaffBuyed;

    #endregion
}
