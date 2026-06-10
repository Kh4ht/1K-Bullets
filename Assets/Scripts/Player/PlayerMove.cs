using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerMove(Player newOwner)
    {
        _owner = newOwner;
    }

    private readonly Player _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public Vector2 Dir { get; private set; }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IUpdate()
    {
        UpdateDir();
    }

    public void IFixedUpdate()
    {
        Move();
    }

    public void IAwake() { }
    public void IStart() { }
    public void IOnEnable() { }
    public void IOnDisable() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
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
        if (_owner.PHealth.HealthCrtl.IsDead)
            return;

        _owner.Rb2d.AddForce(_owner.Stats.MoveSpeed
                             * Time.fixedDeltaTime
                             * Dir.normalized);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
