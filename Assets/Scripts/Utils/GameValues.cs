// █████████████████████████████████████████████████████████████████████████████████████████████████
#region GameConst
// █████████████████████████████████████████████████████████████████████████████████████████████████

using System;
using UnityEngine;

public static class GameConst
{
    public const float COLLISION_HIT_CD = 0.5f;

    public const float GIZMOS_RADIUS_SMALL = 0.04f;
    public const float GIZMOS_RADIUS_MEDIUM = 0.06f;
    public const float GIZMOS_RADIUS_LARGE = 0.08f;

    public const float ANIMATOR_DEFAULT_SPEED = 4f;

    // Camera
    public const float CAMERA_DEFAULT_LERP_SPEED = 5f;
}

#endregion
// █████████████████████████████████████████████████████████████████████████████████████████████████
#region GameStructs
// █████████████████████████████████████████████████████████████████████████████████████████████████
#endregion

[Serializable]
public struct Directions8
{
    public Vector2 east, west, north, south, northEast, northWest, southEast, southWest;

    public void DrawCircles(Vector2 ownerPos)
    {
        Gizmos.DrawSphere(east + ownerPos, GameConst.GIZMOS_RADIUS_SMALL);
        Gizmos.DrawSphere(west + ownerPos, GameConst.GIZMOS_RADIUS_SMALL);
        Gizmos.DrawSphere(north + ownerPos, GameConst.GIZMOS_RADIUS_SMALL);
        Gizmos.DrawSphere(south + ownerPos, GameConst.GIZMOS_RADIUS_SMALL);
        Gizmos.DrawSphere(northEast + ownerPos, GameConst.GIZMOS_RADIUS_SMALL);
        Gizmos.DrawSphere(northWest + ownerPos, GameConst.GIZMOS_RADIUS_SMALL);
        Gizmos.DrawSphere(southEast + ownerPos, GameConst.GIZMOS_RADIUS_SMALL);
        Gizmos.DrawSphere(southWest + ownerPos, GameConst.GIZMOS_RADIUS_SMALL);
    }
}

// █████████████████████████████████████████████████████████████████████████████████████████████████
#region GameEnums
// █████████████████████████████████████████████████████████████████████████████████████████████████

public static class GameEnums
{
    public enum PlayerAnimationState
    {
        Shooting,
        Idle,
        Idle2,
        Idle3,
        Idle4,
        Walk,
        Run,
        Crouch,
        Die,
        Taunt,
        TakeDamage,
        Attack1,
        Attack2,
        Attack3,
        Attack4,
        Attack5,
        AttackRun,
        AttackRun2,
        Special1,
        Special2,

    }

    public enum BulletType
    {
        Straight,
        Following,
    }

    public enum AnimDir
    {
        North = 4,
        South = 3,
        East = 1,
        West = 2,
        NorthEast = 5,
        NorthWest = 6,
        SouthEast = 7,
        SouthWest = 8,
    }

    public enum AnimAttackDir
    {
        NONE,
        North = 3,
        South = 2,
        East = 0,
        West = 1,
        NorthEast = 4,
        NorthWest = 5,
        SouthEast = 6,
        SouthWest = 7,
    }

    public enum AnimAttackState
    {
        StationaryAttack = 1,
        AttackRun = 2,
    }
}

#endregion
// █████████████████████████████████████████████████████████████████████████████████████████████████
#region GameTags
// █████████████████████████████████████████████████████████████████████████████████████████████████

public static class GameTags
{
    public const string PLAYER = "Player";
    public const string ENEMY = "Enemy";
    public const string WALLS = "Walls";
}

#endregion
// █████████████████████████████████████████████████████████████████████████████████████████████████
#region GameSortingLayers
// █████████████████████████████████████████████████████████████████████████████████████████████████

public static class GameSortingLayers
{
    public const string BACKGROUND = "Background";
    public const string BULLETS = "Bullets";
    public const string WALLS = "Walls";
}

#endregion