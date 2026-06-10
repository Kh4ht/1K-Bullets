using KH;
using UnityEngine;

public class EnemyMove : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public EnemyMove(Enemy newOwner)
    {
        _owner = newOwner;
    }

    private readonly Enemy _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████


    public Vector2 Dir { get; private set; }

    private Player _targetPlayer;


    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IStart()
    {
        _targetPlayer = LevelManager.Ins.Player;
    }

    public void IUpdate()
    {
        if (_owner.EHealth.HealthCrtl.IsDead)
        {
            _owner.EAnimator.AnimRunning(false);
            return;
        }

        UpdateDir();
    }

    public void IFixedUpdate()
    {
        Move();
    }

    public void IAwake() { }
    public void IOnEnable() { }
    public void IOnDisable() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void UpdateDir()
    {
        if (_owner == null)
        {
            Debug.LogError("OWNER DESTROYED");
            return;
        }

        Dir = Kh.GetDir(_owner, _targetPlayer);

        if (Dir != Vector2.zero)
        {
            _owner.EAnimator.AnimRunning(true);
            _owner.EAnimator.AnimMoveDir(Dir);
        }
    }

    private void Move()
    {
        if (_owner.EHealth.HealthCrtl.IsDead)
            return;

        _owner.Rb2d.AddForce(_owner.Stats.moveSpeed
            * Time.fixedDeltaTime
            * Dir.normalized);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
