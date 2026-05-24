
using UnityEngine;

public class BurnerPrefab : MonoBehaviour, IClickable
{
    public int status { get; private set; } // 0 = empty, 1 = cooking, 2 = coocked, 3 = burnt

    [SerializeField] SpriteRenderer foodSprite;

    [Header("Timer")]

    [SerializeField] VisualTimer visualTimer;

    private int burnerID = 0;
    private int foodID = 0;

    private float cookingTimeMax = 5f;
    private float burnTimeMax = 5f;

    private float cookingTime = 5f;
    private float burnTime = 5f;

    private Sprite[] foodStatusSprites;

    public void Init(int id, float[] timers)
    {
        burnerID = id;

        cookingTimeMax = timers[0];
        burnTimeMax = timers[1];

        ResetBurner();
    }

    public void UpdateScript()
    {
        if(status == 1)
        {
            cookingTime += Time.deltaTime;
            visualTimer.UpdateTimer(cookingTime, cookingTimeMax);

            if (cookingTime >= cookingTimeMax)
            {
                status = 2;
                foodSprite.sprite = foodStatusSprites[1];
            }
        }
        else if(status == 2)
        {
            burnTime += Time.deltaTime;
            visualTimer.UpdateTimer(burnTime, burnTimeMax);

            if (burnTime >= burnTimeMax)
            {
                status = 3;
                foodSprite.sprite = foodStatusSprites[2];
                visualTimer.SwitchTimerObject(false);
            }
        }
    }

    public void StartCooking(int _foodID, Sprite[] _foodStatusColors)
    {
        foodID = _foodID;
        foodStatusSprites = _foodStatusColors;
        status = 1;
        foodSprite.enabled = true;
        foodSprite.sprite = foodStatusSprites[0];
        visualTimer.SwitchTimerObject(true);
    }

    public void OnClick()
    {
        if(status == 2)
        {
            EventBus.OnCookedFoodClicked?.Invoke(foodID, burnerID);
        }
        else if(status == 3)
        {
            ResetBurner();
        }
    }

    public void ResetBurner()
    {
        foodSprite.enabled = false;
        foodID = 0;
        status = 0;
        cookingTime = 0;
        burnTime = 0;
        visualTimer.SwitchTimerObject(false);
    }
}
