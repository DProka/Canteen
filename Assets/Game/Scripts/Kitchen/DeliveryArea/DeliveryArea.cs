
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

    private FoodParams[] foodParamsArray;

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

    private void SetArrays()
    {
        foodParamsArray = new FoodParams[readyFoodPrefabsArray.Length];
        for (int i = 0; i < foodParamsArray.Length; i++)
        {
            foodParamsArray[i] = new FoodParams { breadNum = -1, foodNum = -1, sauceNum = -1 };
        }
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
        if (OrderManager.Instance.CheckFoodInOrders(foodParamsArray[plateID]))
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
    }

    private void ResetPlateByNum(int num)
    {
        foodParamsArray[num].breadNum = -1;
        foodParamsArray[num].foodNum = -1;
        foodParamsArray[num].sauceNum = -1;
        readyFoodPrefabsArray[num].ResetPrefab();
    }

    private void ResetAllPlates()
    {
        for (int i = 0; i < readyFoodPrefabsArray.Length; i++)
        {
            ResetPlateByNum(i);
        }
    }
    #endregion

    #region Bread Part

    public void SetBreadOnEmptyPlate(int breadNum)
    {
        for (int i = 0; i < foodParamsArray.Length; i++)
        {
            if (foodParamsArray[i].breadNum == -1)
            {
                foodParamsArray[i].breadNum = breadNum;
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
            breadPrefabsArray[i].Init(i, settings.breadsArray[i]);
        }
    }

    #endregion

    #region Food Part

    public void SetFoodOnEmptyBread(int meatNum, int burnerNum)
    {
        for (int i = 0; i < foodParamsArray.Length; i++)
        {
            if (foodParamsArray[i].breadNum != -1 && foodParamsArray[i].foodNum == -1)
            {
                foodParamsArray[i].foodNum = meatNum;
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
        for (int i = 0; i < foodParamsArray.Length; i++)
        {
            if (foodParamsArray[i].foodNum != -1 && foodParamsArray[i].sauceNum == -1)
            {
                foodParamsArray[i].sauceNum = sauceNum;
                readyFoodPrefabsArray[i].UpdateSprite(2, settings.sauceDaubsArray[sauceNum]);
                break;
            }
        }
    }

    private void InitSaucePrefabs()
    {
        for (int i = 0; i < saucePrefabsArray.Length; i++)
        {
            saucePrefabsArray[i].Init(i, settings.saucesArray[i]);
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
