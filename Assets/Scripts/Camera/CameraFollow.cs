using UnityEngine;

// [RequireComponent(typeof(BoxCollider2D))]
public class CameraFollow : MonoBehaviour, IUpdateObserver
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private Transform _target;
    private bool _canFollow = true;

    // Static

    // Components
    // private BoxCollider2D coll;

    // Systems

    // Getters

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void OnEnable()
    {
        UpdateManager.RegisterObserver(this);
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterObserver(this);
    }

    private void Start()
    {
        _target = LevelManager.Ins.Player.transform;
    }

    public void OUpdate()
    {
        CameraFollowTarget();
    }

    public void OFixedUpdate() { }

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
