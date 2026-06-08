using KH;
using UnityEngine;

public class EnemyMove : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public EnemyMove(Enemy newOwner, Rigidbody2D rb2d)
    {
        _owner = newOwner;
        _rb2d = rb2d;
    }

    private readonly Enemy _owner;
    private readonly Rigidbody2D _rb2d;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public float Speed { get; private set; }
    public Vector2 Dir { get; private set; }

    private Player _targetPlayer;


    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IStart()
    {
        _targetPlayer = GameObject.FindGameObjectWithTag(GameTags.PLAYER).GetComponent<Player>();

        SetSpeed(_owner.Data.DefaultMoveSpeed);
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
        if (_owner.EHealth.HealthCrtl.IsDead)
            return;

        Move();
    }

    public void IAwake() { }
    public void IOnEnable() { }
    public void IOnDisable() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
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
        _rb2d.linearVelocity = Speed
            * Time.fixedDeltaTime
            * Dir.normalized;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
        _owner.EAnimator.SetAnimatorSpeed(
            Speed.MoveSpdToAnimatorSpd()
        );
    }

    #endregion
}
