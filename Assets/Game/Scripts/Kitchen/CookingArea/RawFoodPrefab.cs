
using UnityEngine;

public class RawFoodPrefab : MonoBehaviour, IClickable
{
    [SerializeField] SpriteRenderer mainSprite;

    private int foodID;

    public void Init(int id, Sprite sprite)
    {
        foodID = id;
        mainSprite.sprite = sprite;

        Debug.Log($"Initialized {gameObject.name}. ID - {foodID}");
    }

    public void OnClick()
    {
        Debug.Log($"Clicked {gameObject.name}. ID - {foodID}");
        EventBus.OnRawFoodClicked?.Invoke(foodID);
    }
}
