using KH;
using UnityEngine;

public class EnemyAttackAI : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public EnemyAttackAI(Enemy newOwner)
    {
        _owner = newOwner;
    }

    private readonly Enemy _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private Player _targetPlayer;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IStart()
    {
        _targetPlayer = LevelManager.Ins.Player;
    }

    public void IUpdate()
    {
        TryApplyAttack();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void TryApplyAttack()
    {
        if (_owner.Stats.isAttacking)
            return;

        MeleeAttack1();
    }

    private void MeleeAttack1()
    {
        if (!_owner.Data.hasMeleeAttack1)
            return;

        // What Trigger the Attack?
        if (!Kh.SqrDistanceIsLessThan(_owner.transform.position,
                                     _targetPlayer.transform.position,
                                     _owner.Data.meleeAttack1.targetDetectionRadius))
        {
            return;
        }

        _owner.Stats.attackAnimatorSpeed = _owner.Data.meleeAttack1.speed;
        _owner.EAnimator.TriggerAttack1Anim();
        _owner.Stats.isAttacking = true;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void DamagePlayer(KHDamage damage)
    {
        _targetPlayer.PHealth.HealthCrtl.ApplyDamage(damage);

        if (_targetPlayer.PHealth.HealthCrtl.IsDead)
        {
            _targetPlayer.PAnimator.AnimDeathTrigger(-_owner.EMove.Dir);
        }
    }

    #endregion
}
