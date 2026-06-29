
using UnityEngine;

public class MenuButton : MonoBehaviour, IClickable
{
    public int buttonID { get; private set; }

    public void Init(int id)
    {
        buttonID = id;
    }

    public void OnClick()
    {
        Debug.Log($"Button {gameObject.name} Clicked; ID {buttonID}");
        EventBus.OnMenuButtonClicked?.Invoke(buttonID);
    }
}
