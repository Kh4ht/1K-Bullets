using System.Collections.Generic;
using UnityEngine;
using KH;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(CapsuleCollider2D))]
public class Enemy : MonoBehaviour, IUpdateObserver
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static List<Enemy> DisabledEnemies { get; private set; } = new();
    private static GameObject _enemyContainer;

    // Components
    private Rigidbody2D _rb2d;
    private Animator _animator;

    // Systems
    public EnemyMove EMove { get; private set; }
    public EnemyAnimator EAnimator { get; private set; }
    public EnemyHealth EHealth { get; private set; }
    public EnemyCollision ECollision { get; private set; }

    private readonly List<IKHIUnityMethods> _systems = new();

    // Getters
    public EnemyData Data => _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private EnemyData _data;
    [SerializeField] private SliderController _healthBar;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Reset()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        _rb2d.gravityScale = 0;
        _rb2d.freezeRotation = true;
        tag = GameTags.ENEMY;
    }

    private void OnEnable()
    {
        UpdateManager.RegisterObserver(this);

        _systems.KHForEach(p => p.IOnEnable());
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterObserver(this);

        _systems.KHForEach(p => p.IOnDisable());
    }

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _systems.AddRange(new IKHIUnityMethods[]
        {
            EMove = new(this,
                        rb2d: _rb2d),
            ECollision = new(this),
            EAnimator = new(this,
                            healthBar: _healthBar,
                            animator: _animator),
            EHealth = new(this,
                          healthBar: _healthBar),
        });

        _systems.KHForEach(p => p.IAwake());
    }

    private void Start()
    {
        _systems.KHForEach(p => p.IStart());
    }

    public void OUpdate()
    {
        _systems.KHForEach(p => p.IUpdate());
    }

    public void OFixedUpdate()
    {
        _systems.KHForEach(p => p.IFixedUpdate());
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ECollision.OnCollWithPlayer(collision);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
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
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
        DisabledEnemies.Add(this);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region STATIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static Enemy GetOrCreateEnemy(Vector2 spawnPoint, EnemyData enemyData)
    {
        if (_enemyContainer == null)
        {
            _enemyContainer = new GameObject("Enemies");
        }

        if (DisabledEnemies.KHIsEmpty())
        {
            // Case 1: Create New
            return Instantiate(enemyData.Prefab, spawnPoint, Quaternion.Euler(0, 0, 0), _enemyContainer.transform);
        }
        else
        {
            // Get Disabled
            Enemy selectedEnemy = DisabledEnemies[0];

            DisabledEnemies.Remove(selectedEnemy);
            selectedEnemy.gameObject.SetActive(true); // TEST: move this to the last to see if previous lines will work, or you need to set it to active first for them to work.
            selectedEnemy.transform.position = spawnPoint;
            return selectedEnemy.ResetEnemy();
        }
    }

    #endregion
}
