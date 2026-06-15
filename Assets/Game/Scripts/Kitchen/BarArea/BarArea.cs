
using UnityEngine;

public class BarArea : MonoBehaviour
{
    [SerializeField] CanPrefab[] cansArray;
    [SerializeField] GlassFullPrefab[] glassesArray;

    private KitchenSettings settings;

    public void Init(KitchenSettings _kitchenSettings)
    {
        settings = _kitchenSettings;

        EventBus.OnGlassBoxClicked += PutEmptyGlass;
        EventBus.OnCanClicked += StartFillEmptyGlass;
        EventBus.OnFullGlassClicked += CheckDrinkInOrders;

        InitCans();
        InitGlasses();
    }

    public void UpdateScript()
    {
        UpdateGlasses();
    }

    public void ResetPart()
    {
        for (int i = 0; i < glassesArray.Length; i++)
        {
            glassesArray[i].ResetGlass();
        }
    }

    private void CheckDrinkInOrders(int glassID, int drinkID)
    {
        if(OrderManager.Instance.CheckDrinkInOrders(drinkID))
        {
            ResetGlassByNum(glassID);
        }
    }

    #region Cans Part

    private void InitCans()
    {
        for (int i = 0; i < cansArray.Length; i++)
        {
            cansArray[i].Init(i, settings.drinksArray[i]);
        }
    }

    #endregion

    #region Glasses Part

    private void PutEmptyGlass()
    {
        Debug.Log("PutEmptyGlass");

        for (int i = 0; i < glassesArray.Length; i++)
        {
            if (glassesArray[i].status == 0)
            {
                glassesArray[i].SetNewGlass();
                Debug.Log("PutEmptyGlass on: " + i);
                break;
            }
        }
    }

    private void StartFillEmptyGlass(int drinkID)
    {
        for (int i = 0; i < glassesArray.Length; i++)
        {
            if (glassesArray[i].status == 1)
            {
                glassesArray[i].StartFillGlass(drinkID, settings.drinkColorsArray[drinkID]);
                SoundController.Instance.PlaySound(Sound.Drink);
                break;
            }
        }
    }

    private void UpdateGlasses()
    {
        for (int i = 0; i < glassesArray.Length; i++)
        {
            if (glassesArray[i].status == 2)
            {
                glassesArray[i].UpdateGlass();
            }
        }
    }

    private void InitGlasses()
    {
        for (int i = 0; i < glassesArray.Length; i++)
        {
            glassesArray[i].Init(i, settings.glassFillTime);
        }
    }

    private void ResetGlassByNum(int num)
    {
        glassesArray[num].Init(num, settings.glassFillTime);
        //glassesArray[num].ResetGlass();
    }

    #endregion
}
