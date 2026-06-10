using System.Collections.Generic;
using UnityEngine;
using KH;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(CapsuleCollider2D))]

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : ManagedBehaviour, IManagedUpdate, IManagedFixedUpdate
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static List<Enemy> DisabledEnemies { get; private set; } = new();
    public static List<Enemy> EnabledEnemies { get; private set; } = new();
    private static GameObject _enemyContainer;

    // Components
    public Rigidbody2D Rb2d { get; private set; }
    public SpriteRenderer SpriteR { get; private set; }
    public Animator Animator { get; private set; }

    // Systems
    public EnemyMove EMove { get; private set; }
    public EnemyAnimator EAnimator { get; private set; }
    public EnemyHealth EHealth { get; private set; }
    public EnemyCollision ECollision { get; private set; }
    public EnemyStats Stats { get; private set; }

    private readonly List<IKHIUnityMethods> _systems = new();

    // Getters
    public EnemyData Data => _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private EnemyData _data;
    [SerializeField] private SliderController _healthBar;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Reset()
    {
        Rb2d = GetComponent<Rigidbody2D>();

        Rb2d.gravityScale = 0;
        Rb2d.freezeRotation = true;
        Rb2d.linearDamping = GameConst.LINEAR_DAMPING;

        tag = GameTags.ENEMY;
    }

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

    private void Awake()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteR = GetComponent<SpriteRenderer>();

        _systems.AddRange(new IKHIUnityMethods[]
        {
            Stats = new(this),
            EMove = new(this),
            ECollision = new(this),
            EAnimator = new(this),
            EHealth = new(this,
                          healthBar: _healthBar),
        });

        _systems.KHForEach(p => p.IAwake());
    }

    private void Start()
    {
        _systems.KHForEach(p => p.IStart());
    }

    public void ManagedUpdate()
    {
        if (!LevelManager.Ins.LevelActive)
            return;

        _systems.KHForEach(p => p.IUpdate());
    }

    public void ManagedFixedUpdate()
    {
        if (!LevelManager.Ins.LevelActive)
            return;

        _systems.KHForEach(p => p.IFixedUpdate());
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ECollision.OnCollWithPlayer(collision);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    /// <summary>
    /// This is made for the reusable objects to reset their values.
    /// </summary>
    private Enemy ResetEnemy()
    {
        EHealth.HealthCrtl.Revive();

        return this;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
        EnabledEnemies.Remove(this);
        DisabledEnemies.Add(this);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region STATIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static void ResetStaticFields()
    {
        EnabledEnemies.Clear();
        DisabledEnemies.Clear();
        _enemyContainer = null;
    }

    public static Enemy GetOrCreateEnemy(Vector2 spawnPoint, EnemyData enemyData)
    {
        if (_enemyContainer == null)
        {
            _enemyContainer = new GameObject($"Enemies: {EnabledEnemies.Count + DisabledEnemies.Count}");
        }

        if (DisabledEnemies.KHIsEmpty())
        {
            // Case 1: Create New
            Enemy newEnemy = Instantiate(enemyData.Prefab, spawnPoint, Quaternion.Euler(0, 0, 0), _enemyContainer.transform);
            EnabledEnemies.Add(newEnemy);

            _enemyContainer.name = $"Enemies: {EnabledEnemies.Count + DisabledEnemies.Count}";

            return newEnemy;
        }
        else
        {
            // Get Disabled
            Enemy selectedEnemy = DisabledEnemies[0];

            DisabledEnemies.Remove(selectedEnemy);
            EnabledEnemies.Add(selectedEnemy);
            selectedEnemy.gameObject.SetActive(true); // TEST: move this to the last to see if previous lines will work, or you need to set it to active first for them to work.
            selectedEnemy.transform.position = spawnPoint;

            return selectedEnemy.ResetEnemy();
        }
    }

    public static void StopAllEnemiesFromMoving()
    {
        EnabledEnemies.KHForEach((enemy) =>
        {
            enemy.EAnimator.AnimRunning(false);
            enemy.Rb2d.linearVelocity = Vector2.zero;
        });
    }

    #endregion
}
