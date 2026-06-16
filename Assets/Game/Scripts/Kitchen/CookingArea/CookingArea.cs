
using UnityEngine;

public class CookingArea : MonoBehaviour
{
    [SerializeField] BurnerPrefab[] burnersArray;
    [SerializeField] RawFoodPrefab[] rawFoodArray;

    private KitchenSettings settings;

    public void Init(KitchenSettings kitchenSettings)
    {
        settings = kitchenSettings;

        EventBus.OnRawFoodClicked += StartCooking;
        EventBus.OnBurnerReseted += ResetBurner;

        InitBurners();
        InitRawFood();
    }

    public void UpdateScript()
    {
        UpdateBurners();
    }

    public void ResetPart()
    {
        foreach (BurnerPrefab burner in burnersArray)
        {
            burner.ResetBurner();
        }
    }

    #region Raw Food

    private void InitRawFood()
    {
        for (int i = 0; i < rawFoodArray.Length; i++)
        {
            rawFoodArray[i].Init(i);
            rawFoodArray[i].SetSprite(settings.foodSettingsArray[i].foodSpritesArray[0]);
        }

        CheckOpenedRawFood();
    }

    private void CheckOpenedRawFood()
    {
        foreach (RawFoodPrefab pref in rawFoodArray)
        {
            pref.SwitchStatus(KitchenStaffStatus.Closed);
        }

        for (int i = 0; i < PlayerParams.Instance.breadCount; i++)
        {
            rawFoodArray[i].SwitchStatus(KitchenStaffStatus.Open);
        }
    }

    #endregion

    #region Burners

    private void StartCooking(int foodID)
    {
        for (int i = 0; i < burnersArray.Length; i++)
        {
            if (burnersArray[i].status != KitchenStaffStatus.Closed && burnersArray[i].burnerStatus == 0)
            {
                burnersArray[i].StartCooking(foodID, settings.foodSettingsArray[foodID].foodSpritesArray);
                break;
            }
        }
    }

    public void UpdateBurners()
    {
        foreach (BurnerPrefab burner in burnersArray)
        {
            burner.UpdateScript();
        }
    }

    private void ResetBurner(int id)
    {
        burnersArray[id].ResetBurner();
    }

    private void InitBurners()
    {
        for (int i = 0; i < burnersArray.Length; i++)
        {
            float[] timers = new float[2] { settings.maxCookingTime, settings.maxBurningTime };
            burnersArray[i].Init(i);
            burnersArray[i].UpdateParams(timers);
        }

        CheckOpenedBurners();
    }

    private void CheckOpenedBurners()
    {
        foreach (BurnerPrefab pref in burnersArray)
        {
            pref.SwitchStatus(KitchenStaffStatus.Closed);
        }

        for (int i = 0; i < PlayerParams.Instance.breadCount; i++)
        {
            burnersArray[i].SwitchStatus(KitchenStaffStatus.Open);
        }
    }

    #endregion

    private void OnDestroy()
    {
        EventBus.OnRawFoodClicked -= StartCooking;
    }
}
