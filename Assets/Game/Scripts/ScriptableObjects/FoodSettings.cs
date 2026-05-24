
using UnityEngine;

[CreateAssetMenu(fileName = "FoodSettings", menuName = "ScriptableObject/Game/FoodSettings")]
public class FoodSettings : ScriptableObject
{
    public Color[] foodColorsArray;
    public Sprite[] foodSpritesArray;
}
