using KH;
using UnityEngine;

public class EnemyAttackBehaviour : StateMachineBehaviour
{
    private Enemy enemy;
    private bool fired;
    private readonly int fireFrame = 6;
    private int totalFrames;

    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region OnStateEnter
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        if (enemy == null)
            enemy = animator.GetComponent<Enemy>();

        fired = false;
        enemy.AttackColl.enabled = true;

        AnimationClip clip = animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip;
        totalFrames = Mathf.RoundToInt(clip.length * clip.frameRate);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region OnStateUpdate
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public override void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        int currentFrame = Mathf.FloorToInt(
            (stateInfo.normalizedTime % 1f) * totalFrames);

        if (!fired && currentFrame >= fireFrame)
        {
            fired = true;


            if (enemy.AttackColl.IsTouching(LevelManager.Ins.Player.Coll))
            {
                enemy.EAttackAI.DamagePlayer(enemy.Data.meleeAttack1.damage);
                enemy.AttackColl.enabled = false;
            }
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region OnStateExit
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public override void OnStateExit(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        enemy.Stats.isAttacking = false;
    }

    #endregion
}