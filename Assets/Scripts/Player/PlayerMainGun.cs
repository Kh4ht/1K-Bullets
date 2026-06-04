using KH;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainGun : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerMainGun(Player newOwner,
                         BulletData bulletData,
                         Directions8 bulletSpawnPoints)
    {
        _owner = newOwner;
        _bulletData = bulletData;
        _bulletSpawnPoints = bulletSpawnPoints;
    }

    private readonly Player _owner;
    private readonly BulletData _bulletData;
    private readonly Directions8 _bulletSpawnPoints;

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
                Kh.GetDir(_owner.transform.position, GameManager.MouseWorldPos));
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void FireBullet()
    {
        Vector2 playerToMouseDir = Kh.GetDir(_owner.transform.position, GameManager.MouseWorldPos);

        Bullet.GetOrCreateBullet(Helper.DirToBulletSpawnPoint(playerToMouseDir, _bulletSpawnPoints) + _owner.transform.position,
                                 Quaternion.Euler(0, 0, Kh.KHGetAngle(playerToMouseDir)),
                                 _bulletData,
                                 playerToMouseDir,
                                 GameTags.ENEMY);
    }

    #endregion
}
