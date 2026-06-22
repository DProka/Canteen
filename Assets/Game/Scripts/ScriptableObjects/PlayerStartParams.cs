
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStartParams", menuName = "ScriptableObject/Game/PlayerStartParams")]
public class PlayerStartParams : ScriptableObject
{
    public int totalMoneyCounter;
    public int roundMoneyCounter;

    public int tapeCount;
    public int breadCount;
    public int sauceCount;

    public int glassCount;
    public int drinkCount;

    public int burnerCount;
    public int rawFoodCount;
}
