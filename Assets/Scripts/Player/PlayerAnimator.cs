using UnityEngine;

public class PlayerAnimator : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerAnimator(Player newOwner, Animator animator)
    {
        _owner = newOwner;
        _animator = animator;
    }

    private readonly Player _owner;
    private readonly Animator _animator;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public float MoveAnimationSpeed { get; private set; }
    public float AttackAnimationSpeed { get; private set; }

    // float
    private readonly int DIRECTION = Animator.StringToHash("Direction");
    private readonly int ATTACK_STATE = Animator.StringToHash("AttackState");
    private readonly int ATTACK_DIR = Animator.StringToHash("AttackDir");

    // int
    private readonly int DIR_INDEX = Animator.StringToHash("DirIndex");

    // trigger
    private readonly int ATTACK = Animator.StringToHash("Attack");

    // bool
    private readonly int IS_RUN = Animator.StringToHash("IsRun");

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {
        _animator.speed = GameConst.ANIMATOR_DEFAULT_SPEED;
    }

    public void IStart()
    {
        AttackAnimationSpeed = _owner.Data.DefaultAttackSpeed;
    }

    public void IUpdate() { }
    public void IFixedUpdate() { }
    public void IOnEnable() { }
    public void IOnDisable() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void SetMoveAnimationSpeed(float speed)
    {
        MoveAnimationSpeed = speed;
        _animator.speed = MoveAnimationSpeed;
    }
    public void SetAttackAnimationSpeed(float speed)
    {
        AttackAnimationSpeed = speed;
        _animator.speed = AttackAnimationSpeed;
    }

    public void AnimRunning(bool isRunning)
    {
        _animator.SetBool(IS_RUN, isRunning);
    }

    public void AnimMoveDir(Vector2 dir)
    {
        _animator.SetFloat(DIRECTION, (float)Helper.PlayerMoveDirToAnimDir(dir));
    }

    public void AnimAttack(Vector2 dir)
    {
        _animator.SetTrigger(ATTACK);
        _animator.SetFloat(ATTACK_DIR, (float)Helper.Vector2ToAnimDir(dir));
    }

    public void AnimUpdateAttackDir(Vector2 dir)
    {
        _animator.SetFloat(ATTACK_DIR, (float)Helper.Vector2ToAnimDir(dir));
        AnimMoveDir(dir);
    }

    public void AnimAttackState(GameEnums.AnimAttackState animAttackState)
    {
        _animator.SetFloat(ATTACK_STATE, (float)animAttackState);
    }

    #endregion
}
