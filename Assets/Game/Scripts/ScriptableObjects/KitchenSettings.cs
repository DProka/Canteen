
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenSettings", menuName = "ScriptableObject/Game/KitchenSettings")]
public class KitchenSettings : ScriptableObject
{
    [Header("Delivery Area")]

    public Sprite[] breadsArray;
    public Sprite[] breadSlicesArray;

    public Sprite[] saucesArray;
    public Sprite[] sauceDaubsArray;

    [Header("Food Colors")]

    public float maxCookingTime = 3f;
    public float maxBurningTime = 5f;

    public FoodSettings[] foodSettingsArray;

    [Header("Bar Area")]

    public float glassFillTime = 3f;
    public Sprite[] drinksArray;
    public Color[] drinkColorsArray;

    [Header("Orders")]

    public float orderLifeTime = 25f;
}
