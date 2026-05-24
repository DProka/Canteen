
using UnityEngine;

public class BreadPrefab : MonoBehaviour, IClickable
{
    [SerializeField] SpriteRenderer mainSprite;

    private int breadID;

    public void Init(int id, Sprite sprite)
    {
        breadID = id;
        mainSprite.sprite = sprite;
    }

    public void OnClick()
    {
        Debug.Log($"Clicked {gameObject.name}. ID - {breadID}");
        EventBus.OnBreadClicked?.Invoke(breadID);
    }
}
