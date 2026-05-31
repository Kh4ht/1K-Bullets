using KH;
using UnityEngine;

public static class Helper
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region DirToAnimDirIndex
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static GameEnums.AnimAttackDir DirToAnimDirIndex(Vector2 vector2)
    {
        float angle = Kh.KHGetAngle(vector2);

        // Normalize angle to 0-360 range
        angle = angle % 360;
        if (angle < 0) angle += 360;

        // Add half of 45° (22.5°) to center the ranges
        angle += 22.5f;

        if (angle >= 0 && angle < 45)
        {
            return GameEnums.AnimAttackDir.East; // East
        }
        else if (angle >= 45 && angle < 90)
        {
            return GameEnums.AnimAttackDir.NorthEast; // Northeast
        }
        else if (angle >= 90 && angle < 135)
        {
            return GameEnums.AnimAttackDir.North; // North
        }
        else if (angle >= 135 && angle < 180)
        {
            return GameEnums.AnimAttackDir.NorthWest; // Northwest
        }
        else if (angle >= 180 && angle < 225)
        {
            return GameEnums.AnimAttackDir.West; // West
        }
        else if (angle >= 225 && angle < 270)
        {
            return GameEnums.AnimAttackDir.SouthWest; // Southwest
        }
        else if (angle >= 270 && angle < 315)
        {
            return GameEnums.AnimAttackDir.South; // South
        }
        else // (angle >= 315 && angle < 360)
        {
            return GameEnums.AnimAttackDir.SouthEast; // Southeast
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region DirToAnimDirIndex
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static GameEnums.AnimDir DirToAnimDir(Vector2 vector2)
    {
        float angle = Kh.KHGetAngle(vector2);

        // Normalize angle to 0-360 range
        angle = angle % 360;
        if (angle < 0) angle += 360;

        // Add half of 45° (22.5°) to center the ranges
        angle += 22.5f;

        if (angle >= 0 && angle < 45)
        {
            return GameEnums.AnimDir.East; // East
        }
        else if (angle >= 45 && angle < 90)
        {
            return GameEnums.AnimDir.NorthEast; // Northeast
        }
        else if (angle >= 90 && angle < 135)
        {
            return GameEnums.AnimDir.North; // North
        }
        else if (angle >= 135 && angle < 180)
        {
            return GameEnums.AnimDir.NorthWest; // Northwest
        }
        else if (angle >= 180 && angle < 225)
        {
            return GameEnums.AnimDir.West; // West
        }
        else if (angle >= 225 && angle < 270)
        {
            return GameEnums.AnimDir.SouthWest; // Southwest
        }
        else if (angle >= 270 && angle < 315)
        {
            return GameEnums.AnimDir.South; // South
        }
        else // (angle >= 315 && angle < 360)
        {
            return GameEnums.AnimDir.SouthEast; // Southeast
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region DirToAnimDirIndex
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static GameEnums.AnimDir Vector2ToAnimDir(Vector2 vector2)
    {
        // Handle pure directions (no diagonals)
        if (vector2.x == 0)
        {
            if (vector2.y > 0) return GameEnums.AnimDir.North;
            if (vector2.y < 0) return GameEnums.AnimDir.South;
        }

        if (vector2.y == 0)
        {
            if (vector2.x > 0) return GameEnums.AnimDir.East;
            if (vector2.x < 0) return GameEnums.AnimDir.West;
        }

        // Handle diagonals
        if (vector2.x > 0 && vector2.y > 0) return GameEnums.AnimDir.NorthEast;
        if (vector2.x > 0 && vector2.y < 0) return GameEnums.AnimDir.SouthEast;
        if (vector2.x < 0 && vector2.y > 0) return GameEnums.AnimDir.NorthWest;
        if (vector2.x < 0 && vector2.y < 0) return GameEnums.AnimDir.SouthWest;

        // Default (0,0)
        return GameEnums.AnimDir.NONE;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region DirToAnimDirIndex
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static Vector3 DirToBulletSpawnPoint(Vector2 vector2, Directions8 directions8)
    {
        float angle = Kh.KHGetAngle(vector2);

        // Normalize angle to 0-360 range
        angle = angle % 360;
        if (angle < 0) angle += 360;

        // Add half of 45° (22.5°) to center the ranges
        angle += 22.5f;

        if (angle >= 0 && angle < 45)
        {
            return directions8.east; // East
        }
        else if (angle >= 45 && angle < 90)
        {
            return directions8.northEast; // Northeast
        }
        else if (angle >= 90 && angle < 135)
        {
            return directions8.north; // North
        }
        else if (angle >= 135 && angle < 180)
        {
            return directions8.northWest; // Northwest
        }
        else if (angle >= 180 && angle < 225)
        {
            return directions8.west; // West
        }
        else if (angle >= 225 && angle < 270)
        {
            return directions8.southWest; // Southwest
        }
        else if (angle >= 270 && angle < 315)
        {
            return directions8.south; // South
        }
        else // (angle >= 315 && angle < 360)
        {
            return directions8.southEast; // Southeast
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region DirToAnimDirIndex
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // public static void DrawSmallCircle(Vector2 vector2, Directions8 directions8)
    // {

    // }

    #endregion

}