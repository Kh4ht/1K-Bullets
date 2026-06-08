using UnityEngine;

public class BulletMove : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public BulletMove(Bullet newOwner)
    {
        _owner = newOwner;
    }

    private readonly Bullet _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IOnEnable() { }
    public void IOnDisable() { }
    public void IAwake() { /* Cannot be used */ }
    public void IStart() { }
    public void IUpdate() { }
    public void IFixedUpdate()
    {
        if (_owner.Data.defaultBulletType == GameEnums.BulletType.Straight)
        {
            MoveStraight();
        }
        else if (_owner.Data.defaultBulletType == GameEnums.BulletType.Following)
        {
            FollowTarget();
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void MoveStraight()
    {
        _owner.Rb2d.linearVelocity = _owner.States.moveSpeed * Time.fixedDeltaTime * _owner.States.dir.normalized;
    }

    private void FollowTarget()
    {
        // TODO
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
