using KH;
using UnityEngine;

public class EnemyAnimator : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public EnemyAnimator(Enemy newOwner)
    {
        _owner = newOwner;
    }

    private readonly Enemy _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // float
    private readonly int DIRECTION = Animator.StringToHash("Direction");

    // bool
    private readonly int IS_RUN = Animator.StringToHash("IsRun");

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {
        _owner.Animator.speed = GameConst.ANIMATOR_DEFAULT_SPEED;
    }

    public void IUpdate()
    {
        SetAnimatorSpeed();

        _owner.SpriteR.KHUpdateSortingOrderBasedOnYPos(_owner.transform.position.y);
    }

    public void IStart() { }
    public void IFixedUpdate() { }
    public void IOnEnable() { }
    public void IOnDisable() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void SetAnimatorSpeed()
    {
        _owner.Animator.speed = _owner.Rb2d.linearVelocity.MoveSpdToAnimatorSpd();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void AnimRunning(bool isRunning)
    {
        _owner.Animator.SetBool(IS_RUN, isRunning);
    }

    public void AnimMoveDir(Vector2 dir)
    {
        _owner.Animator.SetFloat(DIRECTION, (float)Helper.V2ToAnimDir(dir));
    }


    #endregion
}
