// █████████████████████████████████████████████████████████████████████████████████████████████████
#region GameConst
// █████████████████████████████████████████████████████████████████████████████████████████████████

public static class GameConst
{
    // Use This With LinearVelocity
    public const float DEFAULT_SPEED = 50f;

    public const float COLLISION_HIT_CD = 0.5f;
}

#endregion
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

    public enum Direction
    {
        NONE,
        North = 4,
        South = 3,
        East = 1,
        West = 2,
        NorthEast = 5,
        NorthWest = 6,
        SouthEast = 7,
        SouthWest = 8,
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
}

#endregion