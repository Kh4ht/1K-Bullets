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

    private readonly List<IKHIUnityMethods> _systems = new();

    // Getters
    public PlayerData Data => _data;
    // public GameObject EngineFireVFX => _engineFireVFX;

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
        _rb2d = GetComponent<Rigidbody2D>();

        _rb2d.gravityScale = 0;
        _rb2d.freezeRotation = true;
        tag = GameTags.PLAYER;
    }

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
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _systems.AddRange(new IKHIUnityMethods[]
        {
            PAnimator = new(this, animator: _animator),
            PHealth = new(this,
                          maxHealth: Data.DefaultMaxHealth),
            PMove = new(this,
                        rigidbody2D: _rb2d),
            PMainGun = new(this,
                           bulletData: Data.DefaultBulletData,
                           bulletSpawnPoints: Data.BulletSpawnPoints),
        });

        _systems.AwakeAll();
    }

    private void Start()
    {
        _systems.StartAll();
    }

    public void OUpdate()
    {
        _systems.UpdateAll();
    }

    public void OFixedUpdate()
    {
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
