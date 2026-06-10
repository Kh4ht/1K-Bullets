using UnityEngine;

public class BulletStates : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public BulletStates(Bullet newOwner)
    {
        _owner = newOwner;
    }

    public BulletStates(float moveSpeed,
                        KHDamage damage,
                        int knockBackForce,
                        Vector2 dir = default,
                        string targetTag = default)
    {
        ResetStates(moveSpeed, damage, knockBackForce, dir, targetTag);
    }

    private readonly Bullet _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public int knockBackForce;
    public float moveSpeed;
    public string targetTag;
    public KHDamage damage;
    public Vector2 dir;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {
        ResetStates(_owner.Data.defaultMoveSpeed,
                    _owner.Data.defaultDamage,
                    _owner.Data.defaultKnockBackForce);
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

    public void ResetStates(float moveSpeed,
                            KHDamage damage,
                            int knockBackForce,
                            Vector2 dir = default,
                            string targetTag = default)
    {
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        this.knockBackForce = knockBackForce;
        this.dir = dir;
        this.targetTag = targetTag;
    }

    public void ResetStates(BulletStates bulletStates)
    {
        ResetStates(bulletStates.moveSpeed,
                    bulletStates.damage,
                    bulletStates.knockBackForce,
                    bulletStates.dir,
                    bulletStates.targetTag);
    }
    #endregion
}
