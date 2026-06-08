using System.Collections.Generic;
using UnityEngine;
using KH;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class Bullet : ManagedBehaviour, IManagedUpdate, IManagedFixedUpdate
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static List<Bullet> DisabledBullets { get; private set; } = new();
    private static GameObject _bulletContainer;
    private static int _bulletsCounter = 0;

    // Components
    public Rigidbody2D Rb2d { get; private set; }
    public Collider2D Coll2d { get; private set; }
    public SpriteRenderer SpriteR { get; private set; }

    // Systems
    public BulletMove BMove { get; private set; }
    public BulletCollision BCollision { get; private set; }
    public BulletStates States { get; private set; }

    private readonly List<IKHIUnityMethods> _systems = new();

    // Getters
    public BulletData Data => _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private BulletData _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Reset()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        Coll2d = GetComponent<Collider2D>();

        Rb2d.gravityScale = 0;
        Coll2d.isTrigger = true;
        GetComponent<SpriteRenderer>().sortingLayerName = GameSortingLayers.BULLETS;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BCollision.OnHitTarget(collision);
    }

    private void Awake()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        Coll2d = GetComponent<Collider2D>();
        SpriteR = GetComponent<SpriteRenderer>();

        States = new(this);
        BMove = new(this);
        BCollision = new(this);

        _systems.Clear();
        _systems.AddRange(new IKHIUnityMethods[] { States, BMove, BCollision, });

        _systems.KHForEach(p => p.IAwake());
    }

    private void Start()
    {
        _systems.KHForEach(p => p.IStart());
    }

    public void ManagedUpdate()
    {
        _systems.KHForEach(p => p.IUpdate());
    }

    public void ManagedFixedUpdate()
    {
        _systems.KHForEach(p => p.IFixedUpdate());
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    /// <summary>
    /// This is made for the reusable objects to reset their values.
    /// </summary>
    private Bullet ResetBullet(Vector2 spawnPosition,
                               Quaternion spawnRotation,
                               BulletStates bulletStates)
    {
        gameObject.SetActive(true);

        transform.SetLocalPositionAndRotation(spawnPosition, spawnRotation);

        States.ResetStates(bulletStates);

        return this;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void DisableBullet()
    {
        gameObject.SetActive(false);
        DisabledBullets.Add(this);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region STATIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static void ResetStaticFields()
    {
        DisabledBullets.Clear();
        _bulletsCounter = 0;
        _bulletContainer = null;
    }

    public static Bullet GetOrCreateBullet(Vector2 spawnPosition,
                                           Quaternion spawnRotation,
                                           BulletStates bulletStates,
                                           Bullet prefab)
    {
        if (_bulletContainer == null)
        {
            _bulletContainer = new GameObject($"Bullets: {_bulletsCounter}");
        }

        Bullet selectedBullet;

        if (DisabledBullets.KHIsEmpty())
        {
            _bulletsCounter++;
            _bulletContainer.name = $"Bullets: {_bulletsCounter}";

            // Case 1: Create New Bullet
            selectedBullet = Instantiate(prefab, _bulletContainer.transform);
        }
        else
        {
            // Get Disabled Bullet
            selectedBullet = DisabledBullets[0];

            DisabledBullets.Remove(selectedBullet);
        }

        return selectedBullet.ResetBullet(spawnPosition,
                                          spawnRotation,
                                          bulletStates);
    }

    #endregion
}
