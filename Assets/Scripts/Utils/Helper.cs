using UnityEngine;
using UnityEngine.InputSystem;

public static class Helper
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region GetWASDMovementDir
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static GameEnums.Direction Vector2ToDir(Vector2 vector2)
    {
        // Handle pure directions (no diagonals)
        if (vector2.x == 0)
        {
            if (vector2.y > 0) return GameEnums.Direction.North;
            if (vector2.y < 0) return GameEnums.Direction.South;
        }

        if (vector2.y == 0)
        {
            if (vector2.x > 0) return GameEnums.Direction.East;
            if (vector2.x < 0) return GameEnums.Direction.West;
        }

        // Handle diagonals
        if (vector2.x > 0 && vector2.y > 0) return GameEnums.Direction.NorthEast;
        if (vector2.x > 0 && vector2.y < 0) return GameEnums.Direction.SouthEast;
        if (vector2.x < 0 && vector2.y > 0) return GameEnums.Direction.NorthWest;
        if (vector2.x < 0 && vector2.y < 0) return GameEnums.Direction.SouthWest;

        // Default (0,0)
        return GameEnums.Direction.NONE;
    }

    #endregion
}