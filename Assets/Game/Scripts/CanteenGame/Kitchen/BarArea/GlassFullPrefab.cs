
using UnityEngine;

public class GlassFullPrefab : KitchenStaffPrefab
{
    public int glassStatus { get; private set; } // 0 = clean, 1 = empty, 2 = fill, 3 = full

    [SerializeField] SpriteRenderer backSprite;
    [SerializeField] SpriteRenderer middleSprite;
    [SerializeField] SpriteRenderer frontSprite;

    [SerializeField] VisualTimer visualTimer;

    private int drinkID = 0;

    private float fillTimeMax = 5f;
    private float fillTimer = 5f;

    public override void Init(int _glassID)
    {
        base.Init(_glassID);

        UpdateStatus(0);
    }

    public void UpdateParams(float fillTime)
    {
        fillTimeMax = fillTime;
    }

    public void UpdateGlass()
    {
        if (glassStatus == 2)
        {
            fillTimer += Time.deltaTime;
            visualTimer.UpdateTimer(fillTimer, fillTimeMax);

            if (fillTimer >= fillTimeMax)
            {
                UpdateStatus(3);
            }
        }
    }

    public void SetNewGlass()
    {
        UpdateStatus(1);
    }

    public void StartFillGlass(int _drinkID, Color color)
    {
        drinkID = _drinkID;
        middleSprite.color = color;
        UpdateStatus(2);
    }

    public override void OnClick()
    {
        if(glassStatus == 3)
        {
            Debug.Log("Full glass clicked: " + staffID + " drinkID: " + drinkID);
            EventBus.OnFullGlassClicked?.Invoke(staffID, drinkID);
        }
    }

    public void SetFullGlass(int _drinkID, Color color)
    {
        drinkID = _drinkID;
        middleSprite.color = color;
        middleSprite.enabled = true;
        frontSprite.enabled = true;
        backSprite.enabled = false;
    }

    public void ResetGlass()
    {
        backSprite.enabled = false;
        middleSprite.enabled = false;
        frontSprite.enabled = false;
        fillTimer = 0;
        drinkID = 0;
        glassStatus = 0;
        visualTimer.SwitchTimerObject(false);
    }

    private void UpdateStatus(int _status)
    {
        glassStatus = _status;

        switch (glassStatus)
        {
            case 0:
                ResetGlass();
                break;

            case 1:
                backSprite.enabled = true;
                frontSprite.enabled = true;
                break;

            case 2:
                visualTimer.SwitchTimerObject(true);
                break;

            case 3:
                middleSprite.enabled = true;
                visualTimer.SwitchTimerObject(false);
                break;
        }
    }

}
