using KH;
using UnityEngine;

[DisallowMultipleComponent]
// This manager is responsible for handling queries that need to be accessed by multiple classes.
public class QueryManager : MonoBehaviour, IUpdateObserver
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // public static QueryManager Ins { get; private set; }

    public static Vector2 MouseWorldPos;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Awake()
    {
        // if (Ins == null)
        //     Ins = this;
        // else
        //     Destroy(gameObject);

        DontDestroyOnLoad(this);

        MouseWorldPos = Kh.GetMouseWorldPos();
    }

    private void OnEnable()
    {
        UpdateManager.RegisterObserver(this);
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterObserver(this);
    }

    public void OUpdate()
    {
        MouseWorldPos = Kh.GetMouseWorldPos();
    }

    public void OFixedUpdate()
    {

    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}