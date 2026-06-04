using UnityEngine;

public class EnemyAnimator : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public EnemyAnimator(Enemy newOwner, SliderController healthBar, Animator animator)
    {
        _owner = newOwner;
        _healthBar = healthBar.GetComponent<RectTransform>();
        _animator = animator;
    }

    private readonly Enemy _owner;
    private readonly RectTransform _healthBar;
    private readonly Animator _animator;

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

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {
        _animator.speed = GameConst.ANIMATOR_DEFAULT_SPEED;
    }

    public void IUpdate()
    {

    }

    public void IStart() { }
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

    public void SetAnimatorSpeed(float animatorSpeed)
    {
        _animator.speed = animatorSpeed;
    }

    public void AnimRunning(bool isRunning)
    {
        _animator.SetBool(IS_RUN, isRunning);
    }

    public void AnimMoveDir(Vector2 dir)
    {
        _animator.SetFloat(DIRECTION, (float)Helper.Vector2ToAnimDir(dir));
    }


    #endregion
}
