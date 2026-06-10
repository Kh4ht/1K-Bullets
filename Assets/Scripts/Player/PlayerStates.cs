using UnityEngine;

public class PlayerStats : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerStats(Player newOwner)
    {
        _owner = newOwner;
    }

    private readonly Player _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private bool _isAttacking;
    public bool IsAttacking
    {
        get => _isAttacking;
        set
        {
            _isAttacking = value;

            if (_isAttacking)
            {
                _owner.Animator.speed = attackSpeed;
            }
        }
    }
    public int bulletKnockBackForce;
    public float bulletMoveSpeed;
    public float attackSpeed;
    public KHDamage bulletDamage;

    private float _moveSpeed;
    public float MoveSpeed
    {
        get => _moveSpeed;
        set
        {
            if (value == _moveSpeed)
                return;

            _moveSpeed = value;
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {
        bulletMoveSpeed = _owner.Data.DefaultBulletData.defaultMoveSpeed;
        bulletKnockBackForce = _owner.Data.DefaultBulletData.defaultKnockBackForce;

        bulletDamage = _owner.Data.DefaultBulletData.defaultDamage;
        MoveSpeed = _owner.Data.DefaultMoveSpeed;
        attackSpeed = _owner.Data.DefaultAttackSpeed;
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



    #endregion
}
