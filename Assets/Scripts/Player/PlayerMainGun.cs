using KH;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainGun : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerMainGun(Player newOwner)
    {
        _owner = newOwner;

    }

    private readonly Player _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IUpdate()
    {
        ClickToShoot();
    }

    public void IAwake() { }
    public void IStart() { }
    public void IFixedUpdate() { }
    public void IOnEnable() { }
    public void IOnDisable() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void ClickToShoot()
    {
        if (Mouse.current.leftButton.isPressed && !_owner.Stats.isAttacking)
        {
            _owner.Stats.isAttacking = true;

            _owner.PAnimator.AnimAttack(
                Kh.GetDir(_owner.transform.position, Kh.GetMouseWorldPos()));
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void FireBullet()
    {
        Vector2 playerToMouseDir = Kh.GetDir(_owner.transform.position, Kh.GetMouseWorldPos());

        Bullet.GetOrCreateBullet(Helper.DirToBulletSpawnPoint(playerToMouseDir, _owner.Data.BulletSpawnPoints) + _owner.transform.position,
                                 Quaternion.Euler(0, 0, Kh.KHGetAngle(playerToMouseDir)),
                                 new BulletStates(_owner.Stats.bulletMoveSpeed,
                                                  _owner.Stats.bulletDamage,
                                                  _owner.Stats.bulletKnockBackForce,
                                                  playerToMouseDir,
                                                  GameTags.ENEMY),
                                 _owner.Data.DefaultBulletData.prefab);
    }

    #endregion
}
