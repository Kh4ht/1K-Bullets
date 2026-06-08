using KH;
using UnityEngine;

public class EnemyCollision : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public EnemyCollision(Enemy newOwner)
    {
        _owner = newOwner;
    }

    private readonly Enemy _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private readonly KHTimer _collTimer = new();
    private Player _targetPlayer;

    public KHDamage Damage { get; private set; }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {
        Damage = _owner.Data.DefaultDamage;

    }
    public void IStart()
    {
        _targetPlayer = LevelManager.Ins.Player;
    }

    public void IUpdate()
    {
        _collTimer.Update();
    }

    // Unused
    public void IFixedUpdate() { }
    public void IOnEnable() { }
    public void IOnDisable() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void HitPlayer(Player player)
    {
        player.PHealth.HealthCrtl.ApplyDamage(Damage);

        if (player.PHealth.HealthCrtl.IsDead)
        {
            player.PAnimator.AnimDeathTrigger(-_owner.EMove.Dir);
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void OnCollWithPlayer(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(GameTags.PLAYER))
            return;

        if (_collTimer.DidExceed(GameConst.COLLISION_HIT_CD))
        {
            HitPlayer(_targetPlayer);

            _collTimer.Reset();
        }
    }

    #endregion
}
