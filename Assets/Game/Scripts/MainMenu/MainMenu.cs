
using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] MenuUI menuUI;

    [Header("Buttons")]

    [SerializeField] MenuButton startGameButton;

    private void Awake()
    {
        GameController.CreateIfNeeded();
    }

    private void Start()
    {
        EventBus.OnMenuButtonClicked += MenuButtonClick;
        EventBus.OnTotalMoneyChanged += UpdateUI;

        menuUI.Init();
        UpdateUI();

        startGameButton.Init(1);
    }

    private void Update()
    {

    }

    private void MenuButtonClick(int buttonID)
    {
        switch (buttonID)
        {
            case 1: GoToCanteenGame(); break;
        }
    }

    private void GoToCanteenGame()
    {
        GameController.Instance.LoadSceneByIndex(1);
    }

    #region Menu UI

    private void UpdateUI()
    {
        menuUI.UpdateMenuUI();
    }

    #endregion

    private void OnDestroy()
    {
        EventBus.OnMenuButtonClicked -= MenuButtonClick;
    }
}
