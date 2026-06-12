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

    // int
    private readonly int DIR_INDEX = Animator.StringToHash("DirIndex");

    // Trigger
    private readonly int ATTACK_1 = Animator.StringToHash("Attack1");

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
        UpdateAnimatorSpeed();

        _owner.SpriteR.KHUpdateSortingOrderBasedOnYPos(_owner.transform.position.y);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void UpdateAnimatorSpeed()
    {
        if (_owner.Stats.isAttacking)
        {
            if (_owner.Animator.speed != _owner.Stats.attackAnimatorSpeed)
                _owner.Animator.speed = _owner.Stats.attackAnimatorSpeed;
        }
        else
        {
            _owner.Animator.speed = _owner.Rb2d.linearVelocity.MoveSpdToAnimatorSpd();
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void AnimRunning(bool isRunning)
    {
        _owner.Animator.SetBool(IS_RUN, isRunning);
    }

    public void TriggerAttack1Anim()
    {
        _owner.Animator.SetTrigger(ATTACK_1);
    }

    public void AnimDir(Vector2 dir)
    {
        if (!_owner.Stats.isAttacking)
            _owner.Animator.SetFloat(DIRECTION, (float)Helper.V2ToAnimDir(dir));
        else
            _owner.Animator.SetInteger(DIR_INDEX, (int)Helper.V2ToAnimDirIndex(Kh.GetDir(_owner.transform, LevelManager.Ins.Player.transform)));
    }

    #endregion
}
