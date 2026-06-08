using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : ManagedBehaviour, IManagedUpdate
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static GameManager Ins { get; private set; }
    public static int CurrentLevel { get; private set; }

    // Systems
    private readonly List<IKHIUnityMethods> _systems = new();
    public static CurrencyGM Currency { get; private set; }

    // Getters
    public GameData Data => _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private GameData _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Awake()
    {
        if (Ins == null)
            Ins = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnSceneLoaded;

        CurrentLevel = PlayerPrefs.GetInt(GamePP.CURRENT_LEVEL, 1);

        Currency = new();
        _systems.AddRange(new IKHIUnityMethods[]
        {
            Currency
        });

        _systems.AwakeAll();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void ManagedUpdate()
    {
        _systems.UpdateAll();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // To connect to the system hub
        OnEnable();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void MoveToNextLevel()
    {
        PlayerPrefs.SetInt(GamePP.CURRENT_LEVEL, CurrentLevel + 1);
    }

    #endregion
}
