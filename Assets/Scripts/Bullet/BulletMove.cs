using UnityEngine;

public class BulletMove : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public BulletMove(Bullet newOwner, float newMoveSpeed, GameEnums.BulletType bulletType, Vector2 dir)
    {
        _owner = newOwner;
        MoveSpeed = newMoveSpeed;
        BulletType = bulletType;
        Dir = dir;
    }

    private readonly Bullet _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public float MoveSpeed { get; private set; }
    public GameEnums.BulletType BulletType { get; private set; }
    public Vector2 Dir { get; private set; }

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
        if (BulletType == GameEnums.BulletType.Straight)
        {
            MoveStraight();
        }
        else if (BulletType == GameEnums.BulletType.Following)
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
        _owner.Rb2d.linearVelocity = MoveSpeed * Time.fixedDeltaTime * Dir;
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
