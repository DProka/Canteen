
using UnityEngine;

public class CanPrefab : KitchenStaffPrefab
{
    [SerializeField] SpriteRenderer mainSprite;

    public override void Init(int _canID)
    {
        base.Init(_canID);
    }

    public void SetSprite(Sprite sprite)
    {
        mainSprite.sprite = sprite;
    }

    public override void OnClick()
    {
        EventBus.OnCanClicked?.Invoke(staffID);
    }
}
