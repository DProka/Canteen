
using UnityEngine;

public class KitchenController : MonoBehaviour
{
    [SerializeField] DeliveryArea deliveryArea;
    [SerializeField] CookingArea cookingArea;
    [SerializeField] BarArea barArea;

    private KitchenSettings kitchenSettings;

    public void Init(KitchenSettings settings)
    {
        kitchenSettings = settings;
        deliveryArea.Init(kitchenSettings);
        cookingArea.Init(kitchenSettings);
        barArea.Init(kitchenSettings);
    }

    public void UpdateScript()
    {
        cookingArea.UpdateScript();
        barArea.UpdateScript();
    }

    public void ResetPart()
    {
        deliveryArea.ResetPart();
        cookingArea.ResetPart();
        barArea.ResetPart();
    }
}
