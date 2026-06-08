using UnityEngine;

public class PlayerAnimator : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerAnimator(Player newOwner)
    {
        _owner = newOwner;

    }

    private readonly Player _owner;

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
    private readonly int DIE = Animator.StringToHash("Die");

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
        _owner.Animator.speed = MoveAnimationSpeed;
    }

    public void SetAttackAnimationSpeed(float speed)
    {
        AttackAnimationSpeed = speed;
        _owner.Animator.speed = AttackAnimationSpeed;
    }

    public void AnimRunning(bool isRunning)
    {
        _owner.Animator.SetBool(IS_RUN, isRunning);
    }

    public void AnimMoveDir(Vector2 dir)
    {
        _owner.Animator.SetFloat(DIRECTION, (float)Helper.PlayerMoveDirToAnimDir(dir));
    }

    public void AnimAttack(Vector2 dir)
    {
        _owner.Animator.SetTrigger(ATTACK);
        _owner.Animator.SetFloat(ATTACK_DIR, (float)Helper.V2ToAnimDir(dir));
    }

    public void AnimUpdateAttackDir(Vector2 dir)
    {
        _owner.Animator.SetFloat(ATTACK_DIR, (float)Helper.V2ToAnimDir(dir));
        AnimMoveDir(dir);
    }

    public void AnimAttackState(GameEnums.AnimAttackState animAttackState)
    {
        _owner.Animator.SetFloat(ATTACK_STATE, (float)animAttackState);
    }

    public void AnimDeathTrigger(Vector2 lastDamageTakenDir)
    {
        _owner.Animator.SetTrigger(DIE);
        _owner.Animator.SetInteger(DIR_INDEX, (int)Helper.V2ToAnimDirIndex(lastDamageTakenDir));
    }

    #endregion
}
