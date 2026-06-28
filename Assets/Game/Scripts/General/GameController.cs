
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static SettingsData settings { get; private set; }

    [SerializeField] SettingsData settingsData;

    public static GameController Instance;

    private PlayerParams playerParams;

    public static void CreateIfNeeded()
    {
        if (Instance != null)
            return;

        var prefab = Resources.Load<GameController>("GameControllerPrefab");
        Instantiate(prefab);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    private void Initialize()
    {
        settings = settingsData;

        playerParams = new PlayerParams(settingsData.playerStartParams);

        Debug.Log("GameController = " + Instance.gameObject.name);
    }

    #region Player Params 

    public void UpdateTotalMoneyAfterRound()
    {
        playerParams.UpdateTotalMoneyAfterRound();
    }

    public void AddRoundMoney(int amount) => playerParams.AddRoundMoney(amount);

    #endregion
}
