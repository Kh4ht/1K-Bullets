using UnityEngine;


public class CameraFollow : ManagedBehaviour, IManagedUpdate
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private Transform _target;
    private bool _canFollow = true;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Start()
    {
        _target = LevelManager.Ins.Player.transform;
    }

    public void ManagedUpdate()
    {
        CameraFollowTarget();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void CameraFollowTarget()
    {
        if (!_canFollow)
            return;

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _target.position.x, GameConst.CAMERA_DEFAULT_LERP_SPEED * Time.deltaTime),
                                         Mathf.Lerp(transform.position.y, _target.position.y, GameConst.CAMERA_DEFAULT_LERP_SPEED * Time.deltaTime),
                                         transform.position.z);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region STATIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
