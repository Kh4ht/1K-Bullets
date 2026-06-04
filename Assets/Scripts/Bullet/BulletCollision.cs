using UnityEngine;

public class BulletCollision : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public BulletCollision(Bullet newOwner, string targetTag, float damage)
    {
        _owner = newOwner;
        _targetTag = targetTag;
        _damage = damage;
    }

    private readonly Bullet _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private string _targetTag;
    private float _damage;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IOnEnable() { }
    public void IOnDisable() { }
    public void IAwake() { }
    public void IStart() { }
    public void IUpdate() { }
    public void IFixedUpdate() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void HitPlayer(Player player)
    {
        player.PHealth.HealthCrtl.RemoveHealth(_damage);

        AfterHit();
    }

    private void HitEnemy(Enemy enemy)
    {
        enemy.EHealth.HealthCrtl.RemoveHealth(_damage);

        AfterHit();
    }

    private void AfterHit()
    {
        // Animation & SFX etc..

        _owner.DisableBullet();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void OnHitTarget(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(_targetTag))
            return;

        if (collision.gameObject.TryGetComponent(out Player player))
        {
            HitPlayer(player);
        }
        else if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            HitEnemy(enemy);
        }
    }

    #endregion
}
