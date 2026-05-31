using System.Collections.Generic;
using UnityEngine;
using KH;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour, IUpdateObserver
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Components
    private Rigidbody2D _rb2d;
    private Animator _animator;

    // Systems
    public PlayerMove PMove { get; private set; }
    public PlayerAnimator PAnimator { get; private set; }
    public PlayerMainGun PMainGun { get; private set; }
    public PlayerHealth PHealth { get; private set; }

    private readonly List<KHIUnityMethods> _systems = new();

    // Getters
    public PlayerData Data => _data;
    // public GameObject EngineFireVFX => _engineFireVFX;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private PlayerData _data;
    // [SerializeField] private GameObject _engineFireVFX;
    [SerializeField] private Transform _bulletSpawnPoint;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Reset()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        _rb2d.gravityScale = 0;
        _rb2d.freezeRotation = true;
        tag = GameTags.PLAYER;
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

        _systems.AddRange(new KHIUnityMethods[]
        {
            PAnimator = new(this, animator: _animator),
            PHealth = new(this,
                          maxHealth: Data.DefaultMaxHealth),
            PMove = new(this,
                        rigidbody2D: _rb2d,
                        newSpeed: Data.DefaultMoveSpeed),
            PMainGun = new(this,
                           bulletData: Data.DefaultBulletData,
                           shootCD: Data.DefaultShootCD,
                           bulletSpawnPoint: _bulletSpawnPoint),
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
