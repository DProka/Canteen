
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
            cansArray[i].Init(i);
            cansArray[i].SetSprite(settings.drinksArray[i]);
        }

        CheckOpenedCans();
    }

    private void CheckOpenedCans()
    {
        foreach (CanPrefab pref in cansArray)
        {
            pref.SwitchStatus(KitchenStaffStatus.Closed);
        }

        for (int i = 0; i < PlayerParams.Instance.breadCount; i++)
        {
            cansArray[i].SwitchStatus(KitchenStaffStatus.Open);
        }
    }

    #endregion

    #region Glasses Part

    private void PutEmptyGlass()
    {
        Debug.Log("PutEmptyGlass");

        for (int i = 0; i < glassesArray.Length; i++)
        {
            if (glassesArray[i].status != KitchenStaffStatus.Closed && glassesArray[i].glassStatus == 0)
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
            if (glassesArray[i].glassStatus == 1)
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
            if (glassesArray[i].glassStatus == 2)
            {
                glassesArray[i].UpdateGlass();
            }
        }
    }

    private void InitGlasses()
    {
        for (int i = 0; i < glassesArray.Length; i++)
        {
            //glassesArray[i].Init(i);
            //glassesArray[i].UpdateParams(settings.glassFillTime);
            ResetGlassByNum(i);
        }

        CheckOpenedGlasses();
    }

    private void ResetGlassByNum(int num)
    {
        glassesArray[num].Init(num);
        glassesArray[num].UpdateParams(settings.glassFillTime);
    }

    private void CheckOpenedGlasses()
    {
        foreach (GlassFullPrefab pref in glassesArray)
        {
            pref.SwitchStatus(KitchenStaffStatus.Closed);
        }

        for (int i = 0; i < PlayerParams.Instance.breadCount; i++)
        {
            glassesArray[i].SwitchStatus(KitchenStaffStatus.Open);
        }
    }

    #endregion
}
