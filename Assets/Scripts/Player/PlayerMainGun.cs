using KH;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainGun : KHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerMainGun(Player newOwner,
                         BulletData bulletData,
                         float shootCD,
                         Directions8 bulletSpawnPoints)
    {
        _owner = newOwner;
        _bulletData = bulletData;
        _shootCD = shootCD;
        _bulletSpawnPoints = bulletSpawnPoints;
    }

    private readonly Player _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private BulletData _bulletData;
    private KHTimer _shootCDTimer;
    private float _shootCD;
    private Directions8 _bulletSpawnPoints;

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
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _owner.PAnimator.PlayerAttack(
                Helper.DirToAnimDir(Kh.GetDir(_owner.transform.position, QueryManager.MouseWorldPos)));
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void FireBullet()
    {
        Vector2 playerToMouseDir = Kh.GetDir(_owner.transform.position, QueryManager.MouseWorldPos);

        Bullet.GetOrCreateBullet(Helper.DirToBulletSpawnPoint(playerToMouseDir, _bulletSpawnPoints) + _owner.transform.position,
                                 Quaternion.Euler(0, 0, Kh.KHGetAngle(playerToMouseDir)),
                                 _bulletData,
                                 playerToMouseDir,
                                 GameTags.ENEMY);
    }

    #endregion
}
