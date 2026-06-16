
using UnityEngine;

public class BurnerPrefab : KitchenStaffPrefab
{
    public int burnerStatus { get; private set; } // 0 = empty, 1 = cooking, 2 = coocked, 3 = burnt

    [SerializeField] SpriteRenderer foodSprite;

    [Header("Timer")]

    [SerializeField] VisualTimer visualTimer;

    private int foodID = 0;

    private float cookingTimeMax = 5f;
    private float burnTimeMax = 5f;

    private float cookingTime = 5f;
    private float burnTime = 5f;

    private Sprite[] foodStatusSprites;

    public override void Init(int id)
    {
        base.Init(id);

        ResetBurner();
    }

    public void UpdateScript()
    {
        UpdateCoockindTimer();
        UpdateBurnTimer();
    }

    public void UpdateParams(float[] timers)
    {
        cookingTimeMax = timers[0];
        burnTimeMax = timers[1];
    }

    public void StartCooking(int _foodID, Sprite[] _foodStatusColors)
    {
        foodID = _foodID;
        foodStatusSprites = _foodStatusColors;
        burnerStatus = 1;
        foodSprite.enabled = true;
        foodSprite.sprite = foodStatusSprites[0];
        visualTimer.SwitchTimerObject(true);

        SoundController.Instance.PlaySound(Sound.Burner1);
    }

    public override void OnClick()
    {
        if (burnerStatus == 2)
        {
            EventBus.OnCookedFoodClicked?.Invoke(foodID, staffID);
        }
        else if (burnerStatus == 3)
        {
            ResetBurner();
        }
    }

    public void ResetBurner()
    {
        foodSprite.enabled = false;
        foodID = 0;
        burnerStatus = 0;
        cookingTime = 0;
        burnTime = 0;
        visualTimer.SwitchTimerObject(false);
    }

    #region Timers

    private void UpdateCoockindTimer()
    {
        if (burnerStatus == 1)
        {
            cookingTime += Time.deltaTime;
            visualTimer.UpdateTimer(cookingTime, cookingTimeMax);

            if (cookingTime >= cookingTimeMax)
            {
                burnerStatus = 2;
                foodSprite.sprite = foodStatusSprites[1];
                SoundController.Instance.PlaySound(Sound.Burner2);
            }
        }
    }

    private void UpdateBurnTimer()
    {
        if (burnerStatus == 2)
        {
            burnTime += Time.deltaTime;
            visualTimer.UpdateTimer(burnTime, burnTimeMax);

            if (burnTime >= burnTimeMax)
            {
                burnerStatus = 3;
                foodSprite.sprite = foodStatusSprites[2];
                visualTimer.SwitchTimerObject(false);
            }
        }
    }

    #endregion
}
