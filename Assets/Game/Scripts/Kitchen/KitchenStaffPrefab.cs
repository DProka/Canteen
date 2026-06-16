
using UnityEngine;

public enum KitchenStaffStatus
{
    Closed,
    Open,
    Empty,
    Full,
}

public enum KitchenStaffProcessStatus
{
    Empty,
    Process1,
    Process2,
    Ready,
}

public class KitchenStaffPrefab : MonoBehaviour, IClickable
{
    public int staffID { get; private set; }
    public KitchenStaffStatus status { get; private set; }

    public virtual void Init(int id)
    {
        staffID = id;
    }

    public virtual void SwitchStatus(KitchenStaffStatus newStatus)
    {
        status = newStatus;
        gameObject.SetActive(true);

        switch (status)
        {
            case KitchenStaffStatus.Closed: gameObject.SetActive(false); break;
        }
    }

    public virtual void OnClick()
    {

    }
}
