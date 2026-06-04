using System.Collections.Generic;
using UnityEngine;
using KH;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour, IUpdateObserver
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static List<Bullet> DisabledBullets { get; private set; } = new();
    private static GameObject _bulletContainer;

    // Components
    public Rigidbody2D Rb2d { get; private set; }
    public Collider2D Coll2d { get; private set; }
    public SpriteRenderer SpriteR { get; private set; }

    // Systems
    public BulletMove BMove { get; private set; }
    public BulletCollision BCollision { get; private set; }

    private readonly List<IKHIUnityMethods> _systems = new();

    // Getters

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



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

    private void OnEnable()
    {
        UpdateManager.RegisterObserver(this);
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterObserver(this);
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
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    /// <summary>
    /// This is made for the reusable objects to reset their values.
    /// </summary>
    private Bullet ResetBullet(BulletData bulletData, Vector2 dir, string targetTag)
    {
        BMove = new(this,
                         newMoveSpeed: bulletData.DefaultMoveSpeed,
                         bulletType: bulletData.DefaultBulletType,
                         dir: dir.normalized);

        BCollision = new(this,
                         targetTag: targetTag,
                         damage: bulletData.DefaultDamage);

        _systems.Clear();
        _systems.AddRange(new IKHIUnityMethods[] { BMove, BCollision, });

        SpriteR.sprite = bulletData.Sprite;

        return this;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void DisableBullet()
    {
        gameObject.SetActive(false);
        DisabledBullets.Add(this);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region STATIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static Bullet GetOrCreateBullet(Vector2 spawnPoint, Quaternion rot, BulletData bulletData, Vector2 dir, string targetTag)
    {
        if (_bulletContainer == null)
        {
            _bulletContainer = new GameObject("Bullets");
        }

        if (DisabledBullets.KHIsEmpty())
        {
            // Case 1: Create New Bullet
            return Instantiate(bulletData.Prefab, spawnPoint, rot, _bulletContainer.transform).ResetBullet(bulletData, dir, targetTag);
        }
        else
        {
            // Get Disabled Bullet
            Bullet selectedBullet = DisabledBullets[0];

            DisabledBullets.Remove(selectedBullet);
            selectedBullet.gameObject.SetActive(true); // TEST: move this to the last to see if previous lines will work, or you need to set it to active first for them to work.
            selectedBullet.transform.SetLocalPositionAndRotation(spawnPoint, rot);
            return selectedBullet.ResetBullet(bulletData, dir, targetTag);
        }
    }

    #endregion
}
