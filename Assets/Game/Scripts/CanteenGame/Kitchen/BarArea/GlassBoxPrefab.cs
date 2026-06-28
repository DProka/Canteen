
using UnityEngine;

public class GlassBoxPrefab : MonoBehaviour, IClickable
{
    public void OnClick()
    {
        EventBus.OnGlassBoxClicked?.Invoke();
    }
}
