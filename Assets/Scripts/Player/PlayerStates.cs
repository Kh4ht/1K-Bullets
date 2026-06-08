using UnityEngine;

public class PlayerStates : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerStates(Player newOwner)
    {
        _owner = newOwner;
    }

    private readonly Player _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public float bulletMoveSpeed;
    public KHDamage bulletDamage;

    private float _originalSpeed;
    private float _moveSpeed;
    public float MoveSpeed
    {
        get => _moveSpeed;
        set
        {
            if (value == _moveSpeed)
                return;

            _moveSpeed = value;
            _originalSpeed = _moveSpeed;

            // sync move speed with animation speed.
            _owner.PAnimator.SetMoveAnimationSpeed(
                _moveSpeed.MoveSpdToAnimatorSpd());
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {
        bulletMoveSpeed = _owner.Data.DefaultBulletData.defaultMoveSpeed;
        bulletDamage = _owner.Data.DefaultBulletData.defaultDamage;
        MoveSpeed = _owner.Data.DefaultMoveSpeed;
    }

    public void IOnEnable() { }
    public void IOnDisable() { }
    public void IStart() { }
    public void IUpdate() { }
    public void IFixedUpdate() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void ApplySpeedReductionWhenAttack()
    {
        _owner.States._moveSpeed *= 1 - _owner.Data.DefaultBulletData.defaultSpeedReduction;
    }

    public void RestoreOriginalMoveSpeed()
    {
        _owner.States.MoveSpeed = _originalSpeed;
    }

    #endregion
}
