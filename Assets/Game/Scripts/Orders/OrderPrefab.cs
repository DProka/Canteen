
using DG.Tweening;
using UnityEngine;

public class OrderPrefab : MonoBehaviour
{
    //public int orderID { get; private set; }
    public OrderParams orderParams { get; private set; }

    [SerializeField] ReadyFoodPrefab food;
    [SerializeField] GlassFullPrefab glass;
    [SerializeField] VisualTimer lifeTimer;

    private float maxLifeTime;
    private float lifeTimerValue;

    private bool isActive = false;

    public void Init(OrderParams _orderParams, KitchenSettings settings)//, int id)
    {
        //orderID = id;
        orderParams = _orderParams;
        food.UpdateSprite(0, settings.breadSlicesArray[orderParams.breadID]);
        food.UpdateSprite(1, settings.foodSettingsArray[orderParams.foodID].foodSpritesArray[1]);
        food.UpdateSprite(2, settings.sauceDaubsArray[orderParams.sauceID]);
        glass.SetFullGlass(orderParams.drinkID, settings.drinkColorsArray[orderParams.drinkID]);

        food.gameObject.SetActive(true);
        glass.gameObject.SetActive(true);

        maxLifeTime = settings.orderLifeTime;
        lifeTimerValue = maxLifeTime;

        isActive = false;
        transform.localScale = Vector3.zero;

        Debug.Log("Order initialized: " + orderParams.orderID);
    }

    public bool CheckOrderAlive()
    {
        if(!isActive)
            return true;

        if(lifeTimerValue > 0)
            lifeTimerValue -= Time.deltaTime;

        lifeTimer.UpdateTimer(lifeTimerValue, maxLifeTime);
        return lifeTimerValue > 0;
    }

    public void CloseFood()
    {
        if (!isActive)
            return;

        orderParams.CloseFood();
        food.gameObject.SetActive(false);
    }

    public void CloseDrink()
    {
        if(!isActive)
            return;

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

    #region Animation

    public void StartAppearAnimation()
    {
        transform.DOScale(1, 0.3f).OnComplete(() => 
        {
            isActive = true;
        });
    }

    public void StartDisappearAnimation()
    {
        transform.DOScale(0, 0.3f).OnComplete(() => 
        {
            isActive = false;
            Destroy(gameObject);
        });
    }

    #endregion
}
