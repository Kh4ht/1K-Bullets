using KH;
using UnityEngine;

public class BulletCollision : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public BulletCollision(Bullet newOwner)
    {
        _owner = newOwner;
    }

    private readonly Bullet _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



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
        player.PHealth.HealthCrtl.ApplyDamage(_owner.States.damage);

        AfterHit();
    }

    private void HitEnemy(Enemy enemy)
    {
        enemy.EHealth.HealthCrtl.ApplyDamage(_owner.States.damage);

        if (!enemy.EHealth.HealthCrtl.IsDead)
        {
            enemy.Rb2d.AddForce(_owner.States.knockBackForce * Kh.GetDir(_owner.transform.position,
                                                                         enemy.transform.position));
        }

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
        if (!collision.gameObject.CompareTag(_owner.States.targetTag))
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
