using KH;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : KHIUnityMethods
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

    private readonly int RUNNING_TRIGGER = Animator.StringToHash("Shooting");
    private readonly int IDLE_TRIGGER = Animator.StringToHash("Idle");

    private KHSpriteAnimator _engineFireAnimator;

    public GameEnums.AnimationState AnimationState { get; private set; }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {
        _engineFireAnimator = new(_owner, _owner.EngineFireVFX.GetComponent<SpriteRenderer>());
    }

    public void IStart()
    {
        _engineFireAnimator.PlaySpriteAnimation(_owner.Data.EngineFireSprites, -1);
        _owner.EngineFireVFX.SetActive(false);
    }

    public void IUpdate()
    {
        FlipY();
        UpdateAnimation();
        RotateZ();
        PlayEngineFireAnim();
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
        if (AnimationState != GameEnums.AnimationState.Shooting)
        {
            _owner.Animator.SetTrigger(RUNNING_TRIGGER);
        }
        // Idle Animation
        else if (AnimationState != GameEnums.AnimationState.Idle)
        {
            _owner.Animator.SetTrigger(IDLE_TRIGGER);
        }

    }

    private void FlipY()
    {
        Vector2 dirToMouse = Kh.GetDir(_owner.transform.position, QueryManager.MouseWorldPos);

        if (dirToMouse.x > 0 && _owner.transform.localScale.y != 1)
        {
            _owner.transform.localScale = new Vector3(_owner.transform.localScale.x,
                                                                      1,
                                                                      _owner.transform.localScale.z);
        }
        else if (dirToMouse.x < 0 && _owner.transform.localScale.y != -1)
        {
            _owner.transform.localScale = new Vector3(_owner.transform.localScale.x,
                                                                      -1,
                                                                      _owner.transform.localScale.z);
        }
    }

    private void RotateZ()
    {
        _owner.transform.rotation = Quaternion.Euler(
            _owner.transform.rotation.eulerAngles.x,
            _owner.transform.rotation.eulerAngles.y,
            Kh.KHGetAngle(_owner.transform.position, QueryManager.MouseWorldPos)
        );
    }

    private void PlayEngineFireAnim()
    {
        if (Keyboard.current.wKey.isPressed
            || Keyboard.current.aKey.isPressed
            || Keyboard.current.sKey.isPressed
            || Keyboard.current.dKey.isPressed)

        {
            _owner.EngineFireVFX.SetActive(true);
        }
        else
        {
            _owner.EngineFireVFX.SetActive(false);
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void SetAnimationState(GameEnums.AnimationState newAnimationState)
    {
        if (AnimationState != newAnimationState)
            AnimationState = newAnimationState;
    }

    #endregion
}
