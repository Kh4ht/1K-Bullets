using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerMove(Player newOwner, Rigidbody2D rigidbody2D)
    {
        _owner = newOwner;
        _rb2d = rigidbody2D;
    }

    private readonly Player _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private readonly Rigidbody2D _rb2d;

    public float Speed { get; private set; }
    private float _originalSpeed;
    public Vector2 Dir { get; private set; }


    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {

    }

    public void IStart()
    {
        SetSpeed(_owner.Data.DefaultMoveSpeed);
    }

    public void IUpdate()
    {
        UpdateDir();
    }

    public void IFixedUpdate()
    {
        Move();
    }

    public void IOnEnable() { }
    public void IOnDisable() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void UpdateDir()
    {
        // UP & DOWN
        if (Keyboard.current.wKey.isPressed && Keyboard.current.sKey.isPressed)
            Dir = new Vector2(Dir.x, 0);
        else if (Keyboard.current.wKey.isPressed)
            Dir = new Vector2(Dir.x, 1);
        else if (Keyboard.current.sKey.isPressed)
            Dir = new Vector2(Dir.x, -1);
        else
            Dir = new Vector2(Dir.x, 0);

        // RIGHT & LEFT
        if (Keyboard.current.dKey.isPressed && Keyboard.current.aKey.isPressed)
            Dir = new Vector2(0, Dir.y);
        else if (Keyboard.current.dKey.isPressed)
            Dir = new Vector2(1, Dir.y);
        else if (Keyboard.current.aKey.isPressed)
            Dir = new Vector2(-1, Dir.y);
        else
            Dir = new Vector2(0, Dir.y);

        if (Dir != Vector2.zero)
        {
            // Player moving
            _owner.PAnimator.AnimAttackState(GameEnums.AnimAttackState.AttackRun);
            _owner.PAnimator.AnimRunning(true);
            _owner.PAnimator.AnimMoveDir(Dir);
        }
        else
        {
            _owner.PAnimator.AnimAttackState(GameEnums.AnimAttackState.StationaryAttack);
            _owner.PAnimator.AnimRunning(false);
        }
    }

    private void Move()
    {
        if (Dir == Vector2.zero && _rb2d.linearVelocity == Vector2.zero)
            return;

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
        _originalSpeed = newSpeed;
        Speed = _originalSpeed;

        // sync move speed with animation speed.
        _owner.PAnimator.SetMoveAnimationSpeed(
            Speed.MoveSpdToAnimatorSpd());

    }

    public void ApplySpeedReductionWhenAttack()
    {
        Speed *= 1 - _owner.Data.DefaultBulletData.DefaultSpeedReduction;
    }

    public void RestoreOriginalMoveSpeed()
    {
        Speed = _originalSpeed;
    }

    #endregion
}
