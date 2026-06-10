using System.Collections.Generic;
using KH;
using NaughtyAttributes;
using UnityEngine;

[DisallowMultipleComponent]
// This manager is responsible for handling queries that need to be accessed by multiple classes.
public class LevelManager : ManagedBehaviour, IManagedUpdate, IManagedFixedUpdate
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static LevelManager Ins { get; private set; }

    // Systems
    private readonly List<IKHIUnityMethods> _systems = new();

    public LMEnemySpawner LMEnemySpawner { get; private set; }
    public LMExperience LMExperience { get; private set; }

    // Getters
    public bool IsLevelTimerDone => LevelClock.Seconds >= _data.TimeLimitInSeconds;
    public bool NoEnemiesAlive => Enemy.EnabledEnemies.KHIsEmpty();
    public bool LevelActive { get; private set; } = false;
    public bool LevelPaused { get; private set; } = false;

    public int KillsCounter { get; private set; } = 0;

    public KHTimer LevelClock { get; private set; } = new();
    public Player Player => _player;
    public LevelData Data => _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private Player _player;
    [SerializeField] private Collider2D _levelGroundCollider;


    [HorizontalLine, Header("DATA")]

    [SerializeField] private LevelData _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    protected override void OnEnable()
    {
        base.OnEnable();

        _systems.OnEnableAll();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _systems.OnDisableAll();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_player.transform.position, _data.DefaultSpawnDisFromPlayer);
    }

    private void Awake()
    {
        if (Ins == null)
            Ins = this;
        else
            Destroy(gameObject);

        // Reset Static Fields
        Enemy.ResetStaticFields();
        Bullet.ResetStaticFields();
        ExpGem.ResetStaticFields();

        LMEnemySpawner = new LMEnemySpawner(this,
                                            levelEnemiesData: _data.LevelEnemiesData,
                                            levelGroundCollider: _levelGroundCollider,
                                            player: _player);
        LMExperience = new(this);

        _systems.AddRange(new IKHIUnityMethods[]
        {
            LMEnemySpawner,
            LMExperience
        });

        _systems.AwakeAll();
    }

    private void Start()
    {
        StartLevel();

        _systems.StartAll();
    }

    public void ManagedUpdate()
    {
        if (!LevelActive)
            return;

        if (!IsLevelTimerDone)
        {
            LevelClock.Update();
            LevelUIManager.Ins.UpdateLevelClockTxt((float)LevelClock.Seconds);
        }
        else
        {
            if (NoEnemiesAlive)
            {
                Win();
                // TODO: win logic
            }
        }


        _systems.UpdateAll();
    }

    public void ManagedFixedUpdate()
    {
        _systems.FixedUpdateAll();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
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

    private void Win()
    {
        GameManager.Ins.MoveToNextLevel();

        LevelActive = false;

        LevelUIManager.Ins.WinMenu.gameObject.SetActive(true);

        LevelUIManager.Ins.WinTitle.Play();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

#if UNITY_EDITOR
    [Button("<color='cyan'><b>AddExpPoints</b></color>", EButtonEnableMode.Playmode)]
    public void AddExpPoints()
    {
        LMExperience.AddExpPoints(1);
    }

    [Button("<b>ShowUpgradeCards</b>", EButtonEnableMode.Editor)]
    public void ShowUpgradeCards()
    {
        LMExperience = new(this);
        LMExperience.ShowUpgradeCards();
    }
#endif
    public void SetLevelActive(bool levelActive)
    {
        LevelActive = levelActive;
    }

    public void Lost()
    {
        LevelActive = false;

        LevelUIManager.Ins.LoseMenu.gameObject.SetActive(true);

        LevelUIManager.Ins.DeathTitle.Play();
        // TODO: lost logic
    }

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

    public void TogglePauseLevel()
    {
        LevelPaused = !LevelPaused;

        Time.timeScale = LevelPaused ? 0f : 1f;
    }

    #endregion
}