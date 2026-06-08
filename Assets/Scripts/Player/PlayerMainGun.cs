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

    public bool IsAttacking;

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
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void ClickToShoot()
    {
        if (Mouse.current.leftButton.isPressed && !IsAttacking)
        {
            IsAttacking = true;

            _owner.PAnimator.AnimAttack(
                Kh.GetDir(_owner.transform.position, Kh.GetMouseWorldPos()));
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void FireBullet()
    {
        Vector2 playerToMouseDir = Kh.GetDir(_owner.transform.position, Kh.GetMouseWorldPos());

        Bullet.GetOrCreateBullet(Helper.DirToBulletSpawnPoint(playerToMouseDir, _owner.Data.BulletSpawnPoints) + _owner.transform.position,
                                 Quaternion.Euler(0, 0, Kh.KHGetAngle(playerToMouseDir)),
                                 new BulletStates(_owner.States.bulletMoveSpeed, _owner.States.bulletDamage, playerToMouseDir, GameTags.ENEMY),
                                 _owner.Data.DefaultBulletData.prefab);
    }

    #endregion
}
