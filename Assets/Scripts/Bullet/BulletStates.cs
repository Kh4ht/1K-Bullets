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
                            Vector2 dir = default,
                            string targetTag = default)
    {
        ResetStates(moveSpeed, damage, dir, targetTag);
    }

    private readonly Bullet _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public float moveSpeed;
    public KHDamage damage;
    public Vector2 dir;
    public string targetTag;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {
        ResetStates(_owner.Data.defaultMoveSpeed,
                    _owner.Data.defaultDamage);
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
                            Vector2 dir = default,
                            string targetTag = default)
    {
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        this.dir = dir;
        this.targetTag = targetTag;
    }

    public void ResetStates(BulletStates bulletStates)
    {
        this.moveSpeed = bulletStates.moveSpeed;
        this.damage = bulletStates.damage;
        this.dir = bulletStates.dir;
        this.targetTag = bulletStates.targetTag;
    }
    #endregion
}
