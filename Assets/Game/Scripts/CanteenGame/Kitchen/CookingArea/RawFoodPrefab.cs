
using UnityEngine;

public class RawFoodPrefab : KitchenStaffPrefab
{
    [SerializeField] SpriteRenderer mainSprite;

    public override void Init(int id)
    {
        base.Init(id);
        Debug.Log($"Initialized {gameObject.name}. ID - {staffID}");
    }

    public void SetSprite(Sprite sprite)
    {
        mainSprite.sprite = sprite;
    }

    public override void OnClick()
    {
        Debug.Log($"Clicked {gameObject.name}. ID - {staffID}");
        EventBus.OnRawFoodClicked?.Invoke(staffID);
    }
}
