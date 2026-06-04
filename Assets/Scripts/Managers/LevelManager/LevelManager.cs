using System.Collections.Generic;
using KH;
using UnityEngine;
using UnityEngine.Tilemaps;

[DisallowMultipleComponent]
// This manager is responsible for handling queries that need to be accessed by multiple classes.
public class LevelManager : MonoBehaviour, IUpdateObserver
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static LevelManager Ins { get; private set; }

    // Systems
    private readonly List<IKHIUnityMethods> _systems = new();

    public LMEnemySpawner LMEnemySpawner { get; private set; }

    // Getters
    public bool LevelActive { get; private set; } = false;
    public KHTimer LevelClock { get; private set; } = new();
    public int LevelTimeLimitInSeconds { get; private set; } = 5.MinutesToSeconds();
    public int KillsCounter { get; private set; } = 0;
    public int BulletsShot { get; private set; } = 0;
    public Player Player => _player;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private Player _player;
    [SerializeField] private Collider2D _levelGroundCollider;
    [SerializeField] private List<EnemyData> _levelEnemiesData;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void OnEnable()
    {
        UpdateManager.RegisterObserver(this);

        _systems.OnEnableAll();
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterObserver(this);

        _systems.OnDisableAll();
    }

    private void Awake()
    {
        if (Ins == null)
            Ins = this;
        else
            Destroy(gameObject);

        LMEnemySpawner = new LMEnemySpawner(this,
                                            levelEnemiesData: _levelEnemiesData,
                                            levelGroundCollider: _levelGroundCollider,
                                            player: _player);

        _systems.AddRange(new IKHIUnityMethods[] { LMEnemySpawner, });

        _systems.AwakeAll();
    }

    private void Start()
    {
        StartLevel();

        _systems.StartAll();
    }

    public void OUpdate()
    {
        LevelClock.Update();

        LevelUIManager.Ins.UpdateLevelClockTxt((float)LevelClock.Seconds);

        _systems.UpdateAll();
    }

    public void OFixedUpdate()
    {
        _systems.FixedUpdateAll();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    /// <summary>
    /// 
    /// </summary>
    private void StartLevel()
    {
        LevelClock.Reset();
        KillsCounter = 0;
        LevelActive = true;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    /// <summary>
    /// Call this method whenever an enemy is killed to update the kill count.
    /// </summary>
    public void EnemyKilled()
    {
        if (!LevelActive)
            return;

        KillsCounter++;

        LevelUIManager.Ins.UpdateKillsCounterTxt(KillsCounter);
    }

    /// <summary>
    /// Call this method whenever a bullet is shot to update the shoot counter.
    /// </summary>
    public void BulletShot()
    {
        if (!LevelActive)
            return;

        BulletsShot++;
    }

    #endregion
}