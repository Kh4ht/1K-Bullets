using System;
using KH;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// █████████████████████████████████████████████████████████████████████████████████████████████████
#region Const
// █████████████████████████████████████████████████████████████████████████████████████████████████

public static class GameConst
{
    public const float COLLISION_HIT_CD = 0.5f;

    public const float GIZMOS_RADIUS_SMALL = 0.04f;
    public const float GIZMOS_RADIUS_MEDIUM = 0.06f;
    public const float GIZMOS_RADIUS_LARGE = 0.08f;

    public const float ANIMATOR_DEFAULT_SPEED = 4f;

    // Camera
    public const float CAMERA_DEFAULT_LERP_SPEED = 5f;

    // UI
    public const float UI_SHOW_SPEED = 0.25f;
    public const float UI_HIDE_SPEED = 0.2f;

}

#endregion
// █████████████████████████████████████████████████████████████████████████████████████████████████
#region PlayerPrefs
// █████████████████████████████████████████████████████████████████████████████████████████████████

public static class GamePP // Game PlayerPrefs
{
    public const string CURRENT_LEVEL = "current_level";
    public const string GOLD = "gold";
    public const string RED_CRYSTALS = "red_crystals";

}

#endregion
// █████████████████████████████████████████████████████████████████████████████████████████████████
#region Scenes
// █████████████████████████████████████████████████████████████████████████████████████████████████
#endregion

public static class GameScenes
{
    public const string START = "Start";
    public const string PLAYER_SCREEN = "Player Screen";
    public const string _1 = "1";
}

// █████████████████████████████████████████████████████████████████████████████████████████████████
#region Structs
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

[Serializable]
public struct UpgradeCard
{
    public Button cardButton;
    [Space]
    public Image image;
    public TextMeshProUGUI title;
    public TextMeshProUGUI impact;
    public TextMeshProUGUI description;

    public void SetRandomUpgrade()
    {
        UpgradeCardData pickedCard = GameManager.Ins.Data.upgradeCardDatas.KHPickRandom();

        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(() =>
        {
            pickedCard.ApplyUpgrade();

            LevelManager.Ins.SetLevelActive(true);

            LevelUIManager.Ins.cardsContainer.KH_Hide_Pop();

        });

        image.sprite = pickedCard.sprite;
        title.text = pickedCard.title;
        impact.text = pickedCard.impact;
        description.text = pickedCard.description;
    }
}

// █████████████████████████████████████████████████████████████████████████████████████████████████
#region Enums
// █████████████████████████████████████████████████████████████████████████████████████████████████

public static class GameEnums
{
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

    public enum AnimDirIndex
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
#region Tags
// █████████████████████████████████████████████████████████████████████████████████████████████████

public static class GameTags
{
    public const string PLAYER = "Player";
    public const string ENEMY = "Enemy";
    public const string WALLS = "Walls";
    public const string EXP_GEM = "ExpGem";
}

#endregion
// █████████████████████████████████████████████████████████████████████████████████████████████████
#region SortingLayers
// █████████████████████████████████████████████████████████████████████████████████████████████████

public static class GameSortingLayers
{
    public const string BACKGROUND = "Background";
    public const string BULLETS = "Bullets";
    public const string WALLS = "Walls";
}

#endregion