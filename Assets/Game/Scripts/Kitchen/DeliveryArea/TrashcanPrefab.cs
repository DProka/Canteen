
using UnityEngine;

public class TrashcanPrefab : MonoBehaviour, IClickable
{
    public void OnClick()
    {
        EventBus.OnTrashCanClicked?.Invoke();
    }
}
