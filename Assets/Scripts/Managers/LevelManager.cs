using KH;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
// This manager is responsible for handling queries that need to be accessed by multiple classes.
public class LevelManager : MonoBehaviour, IUpdateObserver
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static LevelManager Ins { get; private set; }

    // Getters
    public bool LevelActive { get; private set; } = false;
    public KHTimer LevelClock { get; private set; } = new();
    public int LevelTimeLimitInSeconds { get; private set; } = 60 * 5; // 5 minutes
    public int KillsCounter { get; private set; } = 0;
    public int BulletsShot { get; private set; } = 0;


    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void OnEnable()
    {
        UpdateManager.RegisterObserver(this);
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterObserver(this);
    }

    void Awake()
    {
        if (Ins == null)
            Ins = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartLevel();
    }

    public void OUpdate()
    {
        if (!LevelActive)
            return;

        LevelClock.Update();

        LevelUIManager.Ins.UpdateLevelClockTxt((float)LevelClock.TimeSeconds);
    }

    public void OFixedUpdate() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    /// <summary>
    /// 
    /// </summary>
    private void StartLevel()
    {
        LevelClock.ResetTimer();
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