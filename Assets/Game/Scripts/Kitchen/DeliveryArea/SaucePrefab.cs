
using UnityEngine;

public class SaucePrefab : MonoBehaviour, IClickable
{
    [SerializeField] SpriteRenderer mainSprite;

    private int sauceID;

    public void Init(int id, Sprite sprite)
    {
        sauceID = id;
        mainSprite.sprite = sprite;
    }

    public void OnClick()
    {
        Debug.Log($"Clicked {gameObject.name}. ID - {sauceID}");
        EventBus.OnSauceClicked?.Invoke(sauceID);
    }
}
