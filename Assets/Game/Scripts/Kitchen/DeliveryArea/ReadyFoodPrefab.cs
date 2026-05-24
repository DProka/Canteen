
using UnityEngine;

public class ReadyFoodPrefab : MonoBehaviour, IClickable
{
    public int plateID { get; private set; }

    [SerializeField] SpriteRenderer[] spritesArray;

    public void Init(int id)
    {
        plateID = id;

        ResetPrefab();
    }

    public void UpdateSprite(int partNum, Sprite sprite)
    {
        spritesArray[partNum].sprite = sprite;
        spritesArray[partNum].enabled = true;
    }

    public void ResetPrefab()
    {
        foreach (SpriteRenderer sprite in spritesArray)
        {
            sprite.color = Color.white;
            sprite.enabled = false;
        }
    }

    public void OnClick()
    {
        Debug.Log("Clicked: " + gameObject.name);
        EventBus.OnReadyFoodClicked?.Invoke(plateID);
    }
}
