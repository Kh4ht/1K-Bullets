using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(CapsuleCollider2D))]
public class Player : ManagedBehaviour, IManagedUpdate, IManagedFixedUpdate
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Components
    public Rigidbody2D Rb2d { get; private set; }
    public Animator Animator { get; private set; }

    // Systems
    public PlayerMove PMove { get; private set; }
    public PlayerAnimator PAnimator { get; private set; }
    public PlayerMainGun PMainGun { get; private set; }
    public PlayerHealth PHealth { get; private set; }
    public PlayerStates States { get; private set; }

    private readonly List<IKHIUnityMethods> _systems = new();

    // Getters
    public PlayerData Data => _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
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

        States = new(this);
        PAnimator = new(this);
        PHealth = new(this);
        PMove = new(this);
        PMainGun = new(this);

        _systems.AddRange(new IKHIUnityMethods[]
        {
            States,
            PAnimator,
            PHealth,
            PMove,
            PMainGun
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
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region STATIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
