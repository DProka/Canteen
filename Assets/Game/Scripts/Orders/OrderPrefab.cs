
using UnityEngine;

public class OrderPrefab : MonoBehaviour
{
    public OrderParams orderParams { get; private set; }

    [SerializeField] ReadyFoodPrefab food;
    [SerializeField] GlassFullPrefab glass;
    [SerializeField] VisualTimer lifeTimer;

    private float maxLifeTime;
    private float lifeTimerValue;

    public void Init(OrderParams _orderParams, KitchenSettings settings)
    {
        orderParams = _orderParams;
        food.UpdateSprite(0, settings.breadSlicesArray[orderParams.breadID]);
        food.UpdateSprite(1, settings.foodSettingsArray[orderParams.foodID].foodSpritesArray[1]);
        food.UpdateSprite(2, settings.sauceDaubsArray[orderParams.sauceID]);
        glass.SetFullGlass(orderParams.drinkID, settings.drinkColorsArray[orderParams.drinkID]);

        food.gameObject.SetActive(true);
        glass.gameObject.SetActive(true);

        maxLifeTime = settings.orderLifeTime;
        lifeTimerValue = maxLifeTime;
    }

    public bool CheckOrderAlive()
    {
        if(lifeTimerValue > 0)
            lifeTimerValue -= Time.deltaTime;

        lifeTimer.UpdateTimer(lifeTimerValue, maxLifeTime);
        return lifeTimerValue > 0;
    }

    public void CloseFood()
    {
        orderParams.CloseFood();
        food.gameObject.SetActive(false);
    }

    public void CloseDrink()
    {
        orderParams.CloseDrink();
        glass.gameObject.SetActive(false);
    }

    public bool CheckOrderComplete()
    {
        int status = 4;

        if (orderParams.foodID == -1)
        {
            status -= 1;
        }
        if (orderParams.breadID == -1)
        {
            status -= 1;
        }
        if (orderParams.sauceID == -1)
        {
            status -= 1;
        }
        if (orderParams.drinkID == -1)
        {
            status -= 1;
        }

        return status == 0;
    }
}
