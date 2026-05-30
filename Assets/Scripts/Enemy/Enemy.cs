using System.Collections.Generic;
using UnityEngine;
using KH;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(CapsuleCollider2D))]
public class Enemy : MonoBehaviour, IUpdateObserver
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Components
    public Rigidbody2D Rb2d { get; private set; }
    public Animator Animator { get; private set; }

    // Systems
    public EnemyMove EMove { get; private set; }
    public EnemyAnimator EAnimator { get; private set; }
    public EnemyHealth EHealth { get; private set; }
    public EnemyCollision ECollision { get; private set; }

    private readonly List<KHIUnityMethods> _systems = new();

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
        Rb2d = GetComponent<Rigidbody2D>();

        Rb2d.gravityScale = 0;
        Rb2d.freezeRotation = true;
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
        Rb2d = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        _systems.AddRange(new KHIUnityMethods[]
        {
            EMove = new(this),
            ECollision = new(this),
            EAnimator = new(this,
                            healthBar: _healthBar),
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
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region STATIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
