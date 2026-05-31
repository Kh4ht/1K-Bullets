using KH;
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

    private readonly int DIRECTION = Animator.StringToHash("Direction");
    private readonly int DIR_INDEX = Animator.StringToHash("DirIndex");
    private readonly int ATTACK1 = Animator.StringToHash("Attack1");
    private readonly int ATTACK2 = Animator.StringToHash("Attack2");
    private readonly int ATTACK3 = Animator.StringToHash("Attack3");
    private readonly int ATTACK4 = Animator.StringToHash("Attack4");
    private readonly int ATTACK5 = Animator.StringToHash("Attack5");
    private readonly int ATTACK_RUN = Animator.StringToHash("AttackRun");
    private readonly int ATTACK_RUN2 = Animator.StringToHash("AttackRun2");
    private readonly int SPECIAL1 = Animator.StringToHash("Special1");
    private readonly int SPECIAL2 = Animator.StringToHash("Special2");
    private readonly int TAUNT = Animator.StringToHash("Taunt");
    private readonly int DIE = Animator.StringToHash("Die");
    private readonly int TAKE_DAMAGE = Animator.StringToHash("TakeDamage");
    private readonly int IS_RUN = Animator.StringToHash("IsRun");
    private readonly int IS_RUN_BACKWARDS = Animator.StringToHash("IsRunBackwards");
    private readonly int IS_WALK = Animator.StringToHash("IsWalk");
    private readonly int IS_STRAFE_LEFT = Animator.StringToHash("IsStrafeLeft");
    private readonly int IS_STRAFE_RIGHT = Animator.StringToHash("IsStrafeRight");
    private readonly int USE_IDLE2 = Animator.StringToHash("UseIdle2");
    private readonly int USE_IDLE3 = Animator.StringToHash("UseIdle3");
    private readonly int USE_IDLE4 = Animator.StringToHash("UseIdle4");
    private readonly int IS_CROUCHING = Animator.StringToHash("IsCrouching");
    private readonly int IS_MOUNTED = Animator.StringToHash("IsMounted");
    private readonly int SPEED_1X = Animator.StringToHash("Speed1x");
    private readonly int SPEED_2X = Animator.StringToHash("Speed2x");



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

    public void IStart()
    {
        // _engineFireAnimator.PlaySpriteAnimation(_owner.Data.EngineFireSprites, -1);
        // _owner.EngineFireVFX.SetActive(false);
    }

    public void IUpdate()
    {
        // FlipY();
        UpdateAnimation();
        // RotateZ();
        // PlayEngineFireAnim();
    }

    // Unused
    public void IFixedUpdate() { }
    public void IOnEnable() { }
    public void IOnDisable() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void UpdateAnimation()
    {




        // Run Animation
        // if (AnimationState != GameEnums.PlayerAnimationState.Run)
        // {
        //     _owner.Animator.SetTrigger(RUNNING_TRIGGER);
        // }
        // // Idle Animation
        // else if (AnimationState != GameEnums.PlayerAnimationState.Idle)
        // {
        //     _owner.Animator.SetTrigger(IDLE_TRIGGER);
        // }
    }



    // private void FlipY()
    // {
    //     Vector2 dirToMouse = Kh.GetDir(_owner.transform.position, QueryManager.MouseWorldPos);

    //     if (dirToMouse.x > 0 && _owner.transform.localScale.y != 1)
    //     {
    //         _owner.transform.localScale = new Vector3(_owner.transform.localScale.x,
    //                                                                   1,
    //                                                                   _owner.transform.localScale.z);
    //     }
    //     else if (dirToMouse.x < 0 && _owner.transform.localScale.y != -1)
    //     {
    //         _owner.transform.localScale = new Vector3(_owner.transform.localScale.x,
    //                                                                   -1,
    //                                                                   _owner.transform.localScale.z);
    //     }
    // }

    // private void RotateZ()
    // {
    //     _owner.transform.rotation = Quaternion.Euler(
    //         _owner.transform.rotation.eulerAngles.x,
    //         _owner.transform.rotation.eulerAngles.y,
    //         Kh.KHGetAngle(_owner.transform.position, QueryManager.MouseWorldPos)
    //     );
    // }

    // private void PlayEngineFireAnim()
    // {
    //     if (Keyboard.current.wKey.isPressed
    //         || Keyboard.current.aKey.isPressed
    //         || Keyboard.current.sKey.isPressed
    //         || Keyboard.current.dKey.isPressed)

    //     {
    //         _owner.EngineFireVFX.SetActive(true);
    //     }
    //     else
    //     {
    //         _owner.EngineFireVFX.SetActive(false);
    //     }
    // }

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

    public void PlayerDir(GameEnums.Direction dir)
    {
        if (dir == GameEnums.Direction.NONE)
            return;

        _animator.SetFloat(DIRECTION, (float)dir);
    }

    #endregion
}
