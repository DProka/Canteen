
using UnityEngine;

public class FoodParams
{
    public int breadNum;
    public int foodNum;
    public int sauceNum;
}

public class DeliveryArea : MonoBehaviour
{
    [Header("Ready Food Prefabs")]

    [SerializeField] ReadyFoodPrefab[] readyFoodPrefabsArray;

    [Header("Bread Prefabs")]

    [SerializeField] BreadPrefab[] breadPrefabsArray;

    [Header("Sauce Prefabs")]

    [SerializeField] SaucePrefab[] saucePrefabsArray;

    private KitchenSettings settings;

    private int pickedPlateNum = -1;

    public void Init(KitchenSettings _kitchenSettings)
    {
        settings = _kitchenSettings;

        EventBus.OnBreadClicked += SetBreadOnEmptyPlate;
        EventBus.OnCookedFoodClicked += SetFoodOnEmptyBread;
        EventBus.OnSauceClicked += SetSausageOnEmptyMeat;
        EventBus.OnTrashCanClicked += MoveFoodToTrash;
        EventBus.OnReadyFoodClicked += CheckFoodInOrders;

        InitBreadPrefabs();
        InitSaucePrefabs();
        InitFoodPrefabs();
    }

    public void ResetPart()
    {
        ResetAllPlates();
    }

    private void MoveFoodToTrash()
    {
        if (pickedPlateNum != -1)
        {
            ResetPlateByNum(pickedPlateNum);
            pickedPlateNum = -1;
        }
    }

    private void CheckFoodInOrders(int plateID)
    {
        if (OrderManager.Instance.CheckFoodInOrders(readyFoodPrefabsArray[plateID].foodParams))
        {
            ResetPlateByNum(plateID);
        }
    }

    #region ReadyFood Part

    private void InitFoodPrefabs()
    {
        SetArrays();

        for (int i = 0; i < readyFoodPrefabsArray.Length; i++)
        {
            readyFoodPrefabsArray[i].Init(i);
        }

        CheckOpenedPlates();
    }

    private void ResetPlateByNum(int num)
    {
        readyFoodPrefabsArray[num].ResetPrefab();
    }

    private void ResetAllPlates()
    {
        for (int i = 0; i < readyFoodPrefabsArray.Length; i++)
        {
            ResetPlateByNum(i);
        }

        CheckOpenedPlates();
    }

    private void SetArrays()
    {
        for (int i = 0; i < readyFoodPrefabsArray.Length; i++)
        {
            FoodParams newParams = new FoodParams { breadNum = -1, foodNum = -1, sauceNum = -1 };
            readyFoodPrefabsArray[i].UpdateFoodParams(newParams);
        }
    }

    private void CheckOpenedPlates()
    {
        foreach (ReadyFoodPrefab pref in readyFoodPrefabsArray)
        {
            pref.SwitchStatus(KitchenStaffStatus.Closed);
        }

        for (int i = 0; i < PlayerParams.Instance.tapeCount; i++)
        {
            readyFoodPrefabsArray[i].SwitchStatus(KitchenStaffStatus.Open);
        }
    }

    #endregion

    #region Bread Part

    public void SetBreadOnEmptyPlate(int breadNum)
    {
        for (int i = 0; i < readyFoodPrefabsArray.Length; i++)
        {
            if (readyFoodPrefabsArray[i].status != KitchenStaffStatus.Closed && readyFoodPrefabsArray[i].foodParams.breadNum == -1)
            {
                readyFoodPrefabsArray[i].foodParams.breadNum = breadNum;
                readyFoodPrefabsArray[i].UpdateSprite(0, settings.breadSlicesArray[breadNum]);
                Debug.Log($"Plate Updated");
                break;
            }
        }
    }

    private void InitBreadPrefabs()
    {
        for (int i = 0; i < breadPrefabsArray.Length; i++)
        {
            breadPrefabsArray[i].Init(i);
            breadPrefabsArray[i].SetSprite(settings.breadsArray[i]);
        }

        CheckOpenedBreads();
    }

    private void CheckOpenedBreads()
    {
        foreach (BreadPrefab pref in breadPrefabsArray)
        {
            pref.SwitchStatus(KitchenStaffStatus.Closed);
        }

        for (int i = 0; i < PlayerParams.Instance.breadCount; i++)
        {
            breadPrefabsArray[i].SwitchStatus(KitchenStaffStatus.Open);
        }
    }

    #endregion

    #region Food Part

    public void SetFoodOnEmptyBread(int meatNum, int burnerNum)
    {
        for (int i = 0; i < readyFoodPrefabsArray.Length; i++)
        {
            if (readyFoodPrefabsArray[i].foodParams.breadNum != -1 && readyFoodPrefabsArray[i].foodParams.foodNum == -1)
            {
                readyFoodPrefabsArray[i].foodParams.foodNum = meatNum;
                readyFoodPrefabsArray[i].UpdateSprite(1, settings.foodSettingsArray[meatNum].foodSpritesArray[1]);
                EventBus.OnBurnerReseted?.Invoke(burnerNum);
                break;
            }
        }
    }

    #endregion

    #region Sauce Part

    public void SetSausageOnEmptyMeat(int sauceNum)
    {
        for (int i = 0; i < readyFoodPrefabsArray.Length; i++)
        {
            if (readyFoodPrefabsArray[i].foodParams.foodNum != -1 && readyFoodPrefabsArray[i].foodParams.sauceNum == -1)
            {
                readyFoodPrefabsArray[i].foodParams.sauceNum = sauceNum;
                readyFoodPrefabsArray[i].UpdateSprite(2, settings.sauceDaubsArray[sauceNum]);
                break;
            }
        }
    }

    private void InitSaucePrefabs()
    {
        for (int i = 0; i < saucePrefabsArray.Length; i++)
        {
            saucePrefabsArray[i].Init(i);
            saucePrefabsArray[i].SetSprite(settings.saucesArray[i]);
        }

        CheckOpenedSauces();
    }

    private void CheckOpenedSauces()
    {
        foreach (SaucePrefab pref in saucePrefabsArray)
        {
            pref.SwitchStatus(KitchenStaffStatus.Closed);
        }

        for (int i = 0; i < PlayerParams.Instance.sauceCount; i++)
        {
            saucePrefabsArray[i].SwitchStatus(KitchenStaffStatus.Open);
        }
    }

    #endregion

    private void OnDestroy()
    {
        EventBus.OnBreadClicked -= SetBreadOnEmptyPlate;
        EventBus.OnCookedFoodClicked -= SetFoodOnEmptyBread;
        EventBus.OnSauceClicked -= SetSausageOnEmptyMeat;
        EventBus.OnTrashCanClicked -= MoveFoodToTrash;
    }
}
