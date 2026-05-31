using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : KHIUnityMethods
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

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

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


    // private KHSpriteAnimator _engineFireAnimator;
    private readonly Animator _animator;

    public GameEnums.PlayerAnimationState AnimationState { get; private set; }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {
        _animator.speed = 4;
        // _engineFireAnimator = new(_owner, _owner.EngineFireVFX.GetComponent<SpriteRenderer>());
    }

    public void IStart() { }
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

    public void SetAnimationState(GameEnums.PlayerAnimationState newAnimationState)
    {
        if (AnimationState != newAnimationState)
            AnimationState = newAnimationState;
    }

    public void PlayerRunning(bool isRunning)
    {
        _animator.SetBool(IS_RUN, isRunning);
    }

    public void PlayerMoveDir(GameEnums.AnimDir animDir)
    {
        if (animDir == GameEnums.AnimDir.NONE)
            return;

        _animator.SetFloat(DIRECTION, (float)animDir);
    }

    public void PlayerAttack(GameEnums.AnimDir animDir)
    {
        _animator.SetTrigger(ATTACK);
        _animator.SetFloat(ATTACK_DIR, (float)animDir);
    }

    public void PlayerUpdateAttackDir(GameEnums.AnimDir animDir)
    {
        _animator.SetFloat(ATTACK_DIR, (float)animDir);
        PlayerMoveDir(animDir);
    }

    public void PlayerAttackDir(GameEnums.AnimAttackState animAttackState)
    {
        _animator.SetFloat(ATTACK_STATE, (float)animAttackState);
    }

    #endregion
}
