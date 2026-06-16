
using UnityEngine;

public class ReadyFoodPrefab : KitchenStaffPrefab
{
    public FoodParams foodParams;

    [SerializeField] SpriteRenderer[] spritesArray;

    public override void Init(int id)
    {
        base.Init(id);

        ResetPrefab();
    }

    public void UpdateFoodParams(FoodParams newParams)
    {
        foodParams = newParams;
    }

    public void UpdateSprite(int partNum, Sprite sprite)
    {
        spritesArray[partNum].sprite = sprite;
        spritesArray[partNum].enabled = true;
    }

    public void ResetPrefab()
    {
        foodParams.breadNum = -1;
        foodParams.foodNum = -1;
        foodParams.sauceNum = -1;

        foreach (SpriteRenderer sprite in spritesArray)
        {
            sprite.color = Color.white;
            sprite.enabled = false;
        }
    }

    public override void OnClick()
    {
        base.OnClick();

        Debug.Log("Clicked: " + gameObject.name);
        EventBus.OnReadyFoodClicked?.Invoke(staffID);
    }
}
