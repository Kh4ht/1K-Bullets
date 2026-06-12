using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(CapsuleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : ManagedBehaviour, IManagedUpdate, IManagedFixedUpdate
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Components
    public Rigidbody2D Rb2d { get; private set; }
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteR { get; private set; }
    public CapsuleCollider2D Coll { get; private set; }

    // Systems
    public PlayerMove PMove { get; private set; }
    public PlayerAnimator PAnimator { get; private set; }
    public PlayerMainGun PMainGun { get; private set; }
    public PlayerHealth PHealth { get; private set; }
    public PlayerStats Stats { get; private set; }

    private readonly List<IKHIUnityMethods> _systems = new();

    // Getters
    public PlayerData Data => _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private PlayerData _data;

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

        tag = GameTags.PLAYER;
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
        Coll = GetComponent<CapsuleCollider2D>();

        _systems.AddRange(new IKHIUnityMethods[]
        {
            Stats = new(this),
            PAnimator = new(this),
            PHealth = new(this),
            PMove = new(this),
            PMainGun = new(this),
        });

        _systems.AwakeAll();
    }

    private void Start()
    {
        _systems.StartAll();
    }

    public void ManagedUpdate()
    {
        if (!LevelManager.Ins.LevelActive)
            return;

        _systems.UpdateAll();
    }

    public void ManagedFixedUpdate()
    {
        if (!LevelManager.Ins.LevelActive)
            return;

        _systems.FixedUpdateAll();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Data.BulletSpawnPoints.DrawCircles(transform.position);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region STATIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
