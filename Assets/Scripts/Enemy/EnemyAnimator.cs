using UnityEngine;

public class EnemyAnimator : KHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public EnemyAnimator(Enemy newOwner, SliderController healthBar)
    {
        _owner = newOwner;
        _healthBar = healthBar.GetComponent<RectTransform>();
    }

    private readonly Enemy _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private readonly int RUNNING_TRIGGER = Animator.StringToHash("Run");
    private readonly RectTransform _healthBar;

    public GameEnums.AnimationState AnimationState { get; private set; }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IUpdate()
    {
        Flip();
        UpdateAnimation();
    }

    // Unused
    public void IAwake() { }
    public void IStart() { }
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
        if (AnimationState != GameEnums.AnimationState.Shooting && _owner.EMove.Dir != Vector2.zero)
        {
            SetAnimationState(GameEnums.AnimationState.Shooting);

            _owner.Animator.SetTrigger(RUNNING_TRIGGER);
        }
    }

    private void SetAnimationState(GameEnums.AnimationState newAnimationState)
    {
        if (AnimationState != newAnimationState)
            AnimationState = newAnimationState;
    }

    private void Flip()
    {
        if (_owner.EMove.Dir.x > 0 && _owner.transform.rotation.y != 0)
        {
            _owner.transform.rotation = Quaternion.Euler(new Vector3(_owner.transform.rotation.x, 0, _owner.transform.rotation.z));
            // Counter-flip the health bar
            _healthBar.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_owner.EMove.Dir.x < 0 && _owner.transform.rotation.y != 180)
        {
            _owner.transform.rotation = Quaternion.Euler(new Vector3(_owner.transform.rotation.x, 180, _owner.transform.rotation.z));
            // Counter-flip the health bar
            _healthBar.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
