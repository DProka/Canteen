
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
    [SerializeField] NpcPrefab npcPrefab;

    private KitchenSettings kitchenSettings;
    private OrderSpawner orderSpawner;
    private NpcSpawner npcSpawner;

    private float timeToNewOrder = 5f;
    private float orderTimer;

    private float[] xPositionsArray;
    private int[] positionStatusArray;

    public void Init(KitchenSettings settings)
    {
        Instance = this;

        kitchenSettings = settings;
        orderSpawner = new OrderSpawner(orderParent, orderPrefab, npcPrefab, kitchenSettings);

        xPositionsArray = new float[] { -7f, -3.5f, 0f, 3.5f, 7f };
        positionStatusArray = new int[5];

        EventBus.OnOrderDelete += ClearPosStatusByNum;

        StartSpawnTimer();
    }

    public void UpdateScript()
    {
        UpdateSpawnTimer();
        orderSpawner.CheckOrdersLifeTimers();
    }

    public void ResetPart()
    {
        orderSpawner.ClearOrders();
        StartSpawnTimer();
        positionStatusArray = new int[5];
    }

    #region Orders

    public void SpawnNewOrder()
    {
        int freePos = GetFreePositionNum();

        if (freePos == -1)
            return;

        orderSpawner.SpawnNewOrder(freePos, xPositionsArray[freePos]);
    }

    public bool CheckFoodInOrders(FoodParams foodParams) => orderSpawner.CheckFoodInOrders(foodParams);

    public bool CheckDrinkInOrders(int drinkParams = 0) => orderSpawner.CheckDrinkInOrders(drinkParams);

    #endregion

    private int GetFreePositionNum()
    {
        List<int> freePos = new List<int>();

        for (int i = 0; i < positionStatusArray.Length; i++)
        {
            if (positionStatusArray[i] == 0)
            {
                freePos.Add(i);
            }
        }

        if (freePos.Count == 0)
            return -1;

        int rand = Random.Range(0, freePos.Count);
        int posNum = freePos[rand];
        positionStatusArray[posNum] = 1;

        return posNum;
    }

    private void ClearPosStatusByNum(int num)
    {
        positionStatusArray[num] = 0;
    }

    #region Spawn Timer

    private void UpdateSpawnTimer()
    {
        //if (orderSpawner.ordersList.Count >= 5)
        if (orderSpawner.CheckListIsFull())
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
