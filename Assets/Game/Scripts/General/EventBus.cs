
using System;
using UnityEngine;

public class EventBus
{
    [Header("General Events")]

    public static Action OnRoundStarted;
    public static Action OnRoundEnded;

    public static Action OnGamePaused;
    public static Action OnGameResumed;

    [Header("Orders Events")]

    public static Action<int> OnNpcStoped;
    public static Action<int> OnOrderDelete;

    [Header("Delivery Events")]

    public static Action<int> OnBreadClicked;
    public static Action<int> OnSauceClicked;
    public static Action<int> OnReadyFoodClicked;
    public static Action<int> OnReadyFoodRemoved;

    public static Action OnTrashCanClicked;

    [Header("Cooking Events")]

    public static Action<int> OnRawFoodClicked;
    public static Action<int, int> OnCookedFoodClicked;
    public static Action<int> OnBurnerReseted;

    [Header("Bar Events")]

    public static Action OnGlassBoxClicked;
    public static Action<int> OnCanClicked;
    public static Action<int, int> OnFullGlassClicked;

    [Header("Combo Events")]

    public static Action OnComboAdded;

    [Header("Player Params Events")]

    public static Action<int> OnTotalMoneyChanged;
    public static Action<int> OnRoundMoneyChanged;
}
