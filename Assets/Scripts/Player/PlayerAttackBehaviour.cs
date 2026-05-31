using KH;
using UnityEngine;

public class PlayerAttackBehaviour : StateMachineBehaviour
{
    private Player player;
    private bool _fired;
    private int fireFrame = 8;
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

        _fired = false;

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
        player.PAnimator.PlayerUpdateAttackDir(
            Helper.DirToAnimDir(
                Kh.GetDir(
                    player.transform.position,
                    QueryManager.MouseWorldPos)));

        int currentFrame = Mathf.FloorToInt(
            (stateInfo.normalizedTime % 1f) * totalFrames);

        if (!_fired && currentFrame >= fireFrame)
        {
            _fired = true;
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

    }

    #endregion
}