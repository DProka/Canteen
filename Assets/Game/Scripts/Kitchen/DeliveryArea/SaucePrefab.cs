
using UnityEngine;

public class SaucePrefab : KitchenStaffPrefab
{
    [SerializeField] SpriteRenderer mainSprite;

    public override void Init(int id)
    {
        base.Init(id);
    }

    public void SetSprite(Sprite sprite)
    {
        mainSprite.sprite = sprite;
    }

    public override void OnClick()
    {
        Debug.Log($"Clicked {gameObject.name}. ID - {staffID}");
        EventBus.OnSauceClicked?.Invoke(staffID);
    }
}
