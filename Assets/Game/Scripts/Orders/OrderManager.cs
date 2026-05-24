
using System.Collections.Generic;
using UnityEngine;

public class OrderParams
{
    public int orderID { get; private set; }

    public int breadID { get; private set; }
    public int foodID { get; private set; }
    public int sauceID { get; private set; }

    public int drinkID { get; private set; }

    public OrderParams(int id, int[] orderParams)
    {
        orderID = id;
        breadID = orderParams[0];
        foodID = orderParams[1];
        sauceID = orderParams[2];
        drinkID = orderParams[3];
    }

    public void CloseFood()
    {
        breadID = -1;
        foodID = -1;
        sauceID = -1;
    }

    public void CloseDrink()
    {
        drinkID = -1;
    }
}

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    [SerializeField] Transform orderParent;
    [SerializeField] OrderPrefab orderPrefab;

    private List<OrderPrefab> ordersList = new List<OrderPrefab>();

    private KitchenSettings kitchenSettings;

    private float orderSpacing = 1.5f;

    private float timeToNewOrder = 5f;
    private float orderTimer;

    public void Init(KitchenSettings settings)
    {
        Instance = this;

        kitchenSettings = settings;

        StartSpawnTimer();
    }

    public void UpdateScript()
    {
        UpdateSpawnTimer();
        CheckOrdersLifeTimers();
    }

    public bool CheckFoodInOrders(FoodParams foodParams)
    {
        for (int i = 0; i < ordersList.Count; i++)
        {
            if (ordersList[i].orderParams.breadID == foodParams.breadNum &&
                ordersList[i].orderParams.foodID == foodParams.foodNum &&
                ordersList[i].orderParams.sauceID == foodParams.sauceNum)
            {
                ordersList[i].CloseFood();
                CheckOrderComplete(i);
                EventBus.OnComboAdded?.Invoke();
                return true;
            }
        }
        return false;
    }

    public bool CheckDrinkInOrders(int drinkParams = 0)
    {
        for (int i = 0; i < ordersList.Count; i++)
        {
            if (ordersList[i].orderParams.drinkID == drinkParams)
            {
                ordersList[i].CloseDrink();
                CheckOrderComplete(i);
                EventBus.OnComboAdded?.Invoke();
                return true;
            }
        }

        return false;
    }

    private void CheckOrderComplete(int orderID)
    {
        if (ordersList[orderID].CheckOrderComplete())
        {
            CloseOrderOnNum(orderID);
        }
    }

    public void ResetPart()
    {
        ClearOrders();
        StartSpawnTimer();
    }

    #region Orders

    public void SpawnNewOrder()
    {
        if (ordersList.Count >= 5)
            return;

        OrderParams newOrderParams = SetNewOrderParams();
        OrderPrefab newOrder = Instantiate(orderPrefab, orderParent);
        newOrder.Init(newOrderParams, kitchenSettings);
        ordersList.Add(newOrder);
        CheckOrdersPositions();
    }

    public void CloseOrder()
    {
        if (ordersList.Count == 0)
            return;

        DeleteOrder(0);
        CheckOrdersPositions();
    }

    public void CloseOrderOnNum(int num)
    {
        if (ordersList.Count == 0)
            return;

        DeleteOrder(num);
        CheckOrdersPositions();

        EventBus.OnRoundMoneyChanged?.Invoke(50);
    }

    private void DeleteOrder(int numInList)
    {
        OrderPrefab order = ordersList[numInList];
        ordersList.RemoveAt(numInList);
        Destroy(order.gameObject);
    }

    private void CheckOrdersPositions()
    {
        for (int i = 0; i < ordersList.Count; i++)
        {
            ordersList[i].transform.localPosition = new Vector3(0, orderSpacing * i, 0);
        }
    }

    private void ClearOrders()
    {
        for (int i = 0; i < ordersList.Count; i++)
        {
            Destroy(ordersList[i].gameObject);
        }
        ordersList.Clear();
    }

    #endregion

    #region Order Randomizer

    private OrderParams SetNewOrderParams()
    {
        int[] food = GetRandomFoodArray();
        int drink = GetRandomDrink();

        return new OrderParams(0, new int[] { food[0], food[1], food[2], drink });
    }

    private int[] GetRandomFoodArray()
    {
        int breadID = Random.Range(0, kitchenSettings.breadSlicesArray.Length);
        int foodID = Random.Range(0, kitchenSettings.foodSettingsArray.Length);
        int sauceID = Random.Range(0, kitchenSettings.sauceDaubsArray.Length);

        return new int[] { breadID, foodID, sauceID };
    }

    private int GetRandomDrink()
    {
        return Random.Range(0, kitchenSettings.drinksArray.Length);
    }

    #endregion

    #region Orders LifeTimer

    private void CheckOrdersLifeTimers()
    {
        for (int i = 0; i < ordersList.Count; i++)
        {
            if (!ordersList[i].CheckOrderAlive())
            {
                DeleteOrder(i);
                CheckOrdersPositions();
            }
        }
    }

    #endregion

    #region Spawn Timer

    private void UpdateSpawnTimer()
    {
        if (ordersList.Count >= 5)
            return;

        orderTimer -= Time.deltaTime;

        if (orderTimer <= 0)
        {
            SpawnNewOrder();
            StartSpawnTimer();
        }
    }

    private void StartSpawnTimer()
    {
        orderTimer = timeToNewOrder;
    }

    #endregion
}
