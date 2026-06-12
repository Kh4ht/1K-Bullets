using KH;
using UnityEngine;

public class PlayerAttackBehaviour : StateMachineBehaviour
{
    private Player player;
    private bool fired;
    private readonly int fireFrame = 8;
    private int totalFrames;

    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region OnStateEnter
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        if (player == null)
            player = animator.GetComponent<Player>();

        fired = false;

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
        player.PAnimator.AnimUpdateAttackDir(
            Kh.GetDir(
                player.transform.position,
                Kh.GetMouseWorldPos()));

        int currentFrame = Mathf.FloorToInt(
            (stateInfo.normalizedTime % 1f) * totalFrames);

        if (!fired && currentFrame >= fireFrame)
        {
            fired = true;
            player.PMainGun.FireBullet();
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
        player.Stats.isAttacking = false;
    }

    #endregion
}