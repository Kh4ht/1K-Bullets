using KH;
using UnityEngine;

public class EnemyMove : KHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public EnemyMove(Enemy newOwner)
    {
        _owner = newOwner;
    }

    private readonly Enemy _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public float speed { get; private set; }
    public Vector2 Dir { get; private set; }

    private Player _targetPlayer;


    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IStart()
    {
        _targetPlayer = GameObject.FindGameObjectWithTag(GameTags.PLAYER).GetComponent<Player>();
    }

    public void IUpdate()
    {
        if (_owner.EHealth.HealthCrtl.IsDead)
            return;

        UpdateDir();
    }

    public void IFixedUpdate()
    {
        if (_owner.EHealth.HealthCrtl.IsDead)
            return;

        Move();
    }

    // Unused
    public void IAwake() { }
    public void IOnEnable() { }
    public void IOnDisable() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void UpdateDir()
    {
        Dir = Kh.GetDir(_owner, _targetPlayer);
    }

    private void Move()
    {
        _owner.Rb2d.linearVelocity = (GameConst.DEFAULT_SPEED + speed)
            * Time.fixedDeltaTime
            * Dir.normalized;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
