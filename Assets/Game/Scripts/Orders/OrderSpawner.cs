
using System.Collections.Generic;
using UnityEngine;

public class OrderRandomizer
{
    public int breadID { get; private set; }
    public int foodID { get; private set; }
    public int sauceID { get; private set; }
    public int drinkID { get; private set; }

    public OrderRandomizer(KitchenSettings settings)
    {
        breadID = Random.Range(0, settings.breadSlicesArray.Length);
        foodID = Random.Range(0, settings.foodSettingsArray.Length);
        sauceID = Random.Range(0, settings.sauceDaubsArray.Length);
        drinkID = Random.Range(0, settings.drinksArray.Length);
    }
}

public class OrderSpawner
{
    private KitchenSettings kitchenSettings;
    private Transform orderParent;

    private OrderPrefab orderPrefab;
    private List<OrderPrefab> ordersList;

    private NpcPrefab npcPrefab;
    private List<NpcPrefab> npcPrefabsList;


    public OrderSpawner(Transform parent, OrderPrefab orderPref, NpcPrefab npcPref, KitchenSettings settings)
    {
        orderParent = parent;
        orderPrefab = orderPref;
        npcPrefab = npcPref;
        kitchenSettings = settings;

        EventBus.OnNpcStoped += OpenOrderByNum;

        ordersList = new List<OrderPrefab>();
        npcPrefabsList = new List<NpcPrefab>();
    }

    public bool CheckListIsFull()
    {
        return ordersList.Count >= 5;
    }

    #region Food Check

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
            CloseOrderByID(orderID);
        }
    }

    #endregion

    #region Orders

    public void SpawnNewOrder(int nextID, float posX)
    {
        if (ordersList.Count >= 5)
            return;

        OrderParams newOrderParams = SetNewOrderParams(nextID);
        OrderPrefab newOrder = Object.Instantiate(orderPrefab, new Vector2(posX, 3.5f), Quaternion.identity, orderParent);
        newOrder.Init(newOrderParams, kitchenSettings);//, nextID);
        ordersList.Add(newOrder);

        SpawnNpc(nextID, posX);
    }

    public void CloseOrderByID(int id)
    {
        if (ordersList.Count == 0)
            return;

        //for (int i = 0; i < ordersList.Count; i++)
        //{
        //if (ordersList[i].orderParams.orderID == id)
        //{
        //DeleteOrder(i);
        //}
        //}

        DeleteOrder(id);
        EventBus.OnRoundMoneyChanged?.Invoke(50);
    }

    public void OpenOrderByNum(int num)
    {
        if (ordersList.Count == 0)
            return;

        for (int i = 0; i < ordersList.Count; i++)
        {
            if (ordersList[i].orderParams.orderID == num)
                ordersList[i].StartAppearAnimation();
        }
    }

    public void CheckOrdersLifeTimers()
    {
        for (int i = 0; i < ordersList.Count; i++)
        {
            if (!ordersList[i].CheckOrderAlive())
            {
                DeleteOrder(i);
            }
        }
    }

    private void DeleteOrder(int numInList)
    {
        OrderPrefab order = ordersList[numInList];
        ordersList.RemoveAt(numInList);
        order.StartDisappearAnimation();
        RemoveNpc(numInList);

        EventBus.OnOrderDelete?.Invoke(order.orderParams.orderID);
    }

    private OrderParams SetNewOrderParams(int id)
    {
        OrderRandomizer orderRandomizer = new OrderRandomizer(kitchenSettings);

        return new OrderParams(id, new int[]
        { orderRandomizer.breadID, orderRandomizer.foodID, orderRandomizer.sauceID, orderRandomizer.drinkID });
    }

    #endregion

    #region NPC

    private void SpawnNpc(int id, float emptyPositionX)
    {
        float npcSpawnPosX = CheckNPCSpawnPosX(emptyPositionX);

        NpcPrefab newNPC = Object.Instantiate(npcPrefab, new Vector2(npcSpawnPosX, -4), Quaternion.identity, orderParent);
        newNPC.Init(id, npcSpawnPosX);
        newNPC.StartWalkToPoint(new Vector2(emptyPositionX, -4), 5f);
        npcPrefabsList.Add(newNPC);
    }

    private void RemoveNpc(int id)
    {
        npcPrefabsList[id].StartWalkToExtit();
        npcPrefabsList.RemoveAt(id);
    }

    private float CheckNPCSpawnPosX(float endPos)
    {
        if (endPos <= 0)
            return -12f;
        else
            return 12f;
    }

    #endregion

    #region Reset Part

    public void ClearOrders()
    {
        for (int i = 0; i < ordersList.Count; i++)
        {
            Object.Destroy(ordersList[i].gameObject);
        }
        ordersList.Clear();

        ResetNPCList();
    }

    private void ResetNPCList()
    {
        for (int i = 0; i < npcPrefabsList.Count; i++)
        {
            Object.Destroy(npcPrefabsList[i].gameObject);
        }
        npcPrefabsList.Clear();
    }

    #endregion
}
