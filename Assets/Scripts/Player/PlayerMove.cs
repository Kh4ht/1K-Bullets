using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : KHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerMove(Player newOwner, float newSpeed)
    {
        _owner = newOwner;
        Speed = newSpeed;
    }

    private readonly Player _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public float Speed { get; private set; }
    public Vector2 Dir { get; private set; }


    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IAwake()
    {

    }

    public void IStart()
    {

    }

    public void IUpdate()
    {
        UpdateDir();
    }

    public void IFixedUpdate()
    {
        Move();
    }

    // Unused
    public void IOnEnable() { }
    public void IOnDisable() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void UpdateDir()
    {
        // UP & DOWN
        if (Keyboard.current.wKey.isPressed && Keyboard.current.sKey.isPressed)
            Dir = new Vector2(Dir.x, 0);
        else if (Keyboard.current.wKey.isPressed)
            Dir = new Vector2(Dir.x, 1);
        else if (Keyboard.current.sKey.isPressed)
            Dir = new Vector2(Dir.x, -1);
        else
            Dir = new Vector2(Dir.x, 0);

        // RIGHT & LEFT
        if (Keyboard.current.dKey.isPressed && Keyboard.current.aKey.isPressed)
            Dir = new Vector2(0, Dir.y);
        else if (Keyboard.current.dKey.isPressed)
            Dir = new Vector2(1, Dir.y);
        else if (Keyboard.current.aKey.isPressed)
            Dir = new Vector2(-1, Dir.y);
        else
            Dir = new Vector2(0, Dir.y);
    }

    private void Move()
    {
        _owner.Rb2d.linearVelocity = (GameConst.DEFAULT_SPEED + Speed)
            * Time.fixedDeltaTime
            * Dir.normalized;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
