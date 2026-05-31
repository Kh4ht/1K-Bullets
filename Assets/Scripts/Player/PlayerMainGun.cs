using KH;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainGun : KHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerMainGun(Player newOwner, BulletData bulletData, float shootCD, Transform bulletSpawnPoint)
    {
        _owner = newOwner;
        _bulletData = bulletData;
        _shootCD = shootCD;
        _bulletSpawnPoint = bulletSpawnPoint;
    }

    private readonly Player _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private BulletData _bulletData;
    private KHTimer _shootCDTimer;
    private float _shootCD;
    private Transform _bulletSpawnPoint;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████


    public void IUpdate()
    {
        ClickToShoot();
    }

    // Unused
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
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // _owner.PAnimator.SetAnimationState(GameEnums.PlayerAnimationState.Shooting);

            // Bullet.GetOrCreateBullet(_bulletSpawnPoint, _bulletData, _bulletSpawnPoint.position.KHGetDirTo(QueryManager.MouseWorldPos));
            Bullet.GetOrCreateBullet(_bulletSpawnPoint, _bulletData, Kh.GetDir(_owner.transform.eulerAngles.z), GameTags.ENEMY);
        }
        // else
        // {
        // _owner.PAnimator.SetAnimationState(GameEnums.PlayerAnimationState.Idle);
        // }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
