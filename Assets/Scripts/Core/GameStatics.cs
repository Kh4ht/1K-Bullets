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
    public enum AnimationState
    {
        Shooting,
        Idle,
    }

    public enum BulletType
    {
        Straight,
        Following,
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