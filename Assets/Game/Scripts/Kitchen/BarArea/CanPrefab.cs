
using UnityEngine;

public class CanPrefab : MonoBehaviour, IClickable
{
    [SerializeField] SpriteRenderer mainSprite;

    private int canID;

    public void Init(int _canID, Sprite sprite)
    {
        canID = _canID;
        mainSprite.sprite = sprite;
    }

    public void OnClick()
    {
        EventBus.OnCanClicked?.Invoke(canID);
    }
}
